﻿using System.Windows.Forms;

namespace Diablo3Kit.Hook;

/// <summary>
///     Key press data.
/// </summary>
public class KeyInputEventArgs : EventArgs
{
    public KeyData KeyData { get; set; }
}

/// <summary>
///     Key data.
/// </summary>
public class KeyData
{
    public KeyEvent EventType;
    public string Keyname;
    public string UnicodeCharacter;
}

/// <summary>
///     Key press event type.
/// </summary>
public enum KeyEvent
{
    down = 0,
    up = 1
}


/// <summary>
///     A class to create a dummy message pump if we don't have one
///     A message pump is required for most of our hooks to succeed
/// </summary>
internal class SyncFactory : IDisposable
{
    private readonly Lazy<MessageHandler> messageHandler;

    private readonly Lazy<TaskScheduler> scheduler;
    private bool hasUIThread;

    internal SyncFactory()
    {
        scheduler = new Lazy<TaskScheduler>(() =>
        {
            //if the calling thread is a UI thread then return its synchronization context
            //no need to create a message pump
            var dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
            if (dispatcher != null)
            {
                if (SynchronizationContext.Current != null)
                {
                    hasUIThread = true;
                    return TaskScheduler.FromCurrentSynchronizationContext();
                }
            }

            TaskScheduler current = null;

            //if current task scheduler is null, create a message pump 
            //http://stackoverflow.com/questions/2443867/message-pump-in-net-windows-service
            //use async for performance gain!
            new Task(() =>
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(
                    new Action(() =>
                    {
                        Volatile.Write(ref current, TaskScheduler.FromCurrentSynchronizationContext());
                    }), DispatcherPriority.Normal);
                Dispatcher.Run();
            }).Start();

            //we called dispatcher begin invoke to get the Message Pump Sync Context
            //we check every 10ms until synchronization context is copied
            while (Volatile.Read(ref current) == null)
            {
                Thread.Sleep(10);
            }

            return Volatile.Read(ref current);
        });

        messageHandler = new Lazy<MessageHandler>(() =>
        {
            MessageHandler msgHandler = null;
            //get the mesage handler dummy window created using the UI sync context
            new Task(e => { Volatile.Write(ref msgHandler, new MessageHandler()); }, GetTaskScheduler()).Start();

            //wait here until the window is created on UI thread
            while (Volatile.Read(ref msgHandler) == null)
            {
                Thread.Sleep(10);
            }

            ;

            return Volatile.Read(ref msgHandler);
        });

        Initialize();
    }

    public void Dispose()
    {
        if (messageHandler?.Value != null)
        {
            messageHandler.Value.DestroyHandle();
        }
    }

    /// <summary>
    ///     Initialize the required message pump for all the hooks
    /// </summary>
    private void Initialize()
    {
        GetTaskScheduler();
        GetHandle();
    }

    /// <summary>
    ///     Get the UI task scheduler
    /// </summary>
    /// <returns></returns>
    internal TaskScheduler GetTaskScheduler()
    {
        return scheduler.Value;
    }

    /// <summary>
    ///     Get the handle of the window we created on the UI thread
    /// </summary>
    /// <returns></returns>
    internal IntPtr GetHandle()
    {
        var handle = IntPtr.Zero;

        if (hasUIThread)
        {
            try
            {
                handle = Process.GetCurrentProcess().MainWindowHandle;

                if (handle != IntPtr.Zero)
                {
                    return handle;
                }
            }
            catch
            {
            }
        }

        return messageHandler.Value.Handle;
    }
}




/// <summary>
///     A dummy class to create a dummy invisible window object
/// </summary>
internal class MessageHandler : NativeWindow
{
    internal MessageHandler()
    {
        CreateHandle(new CreateParams());
    }

    protected override void WndProc(ref Message msg)
    {
        base.WndProc(ref msg);
    }
}


internal class AsyncConcurrentQueue<T>
{
    /// <summary>
    ///     Backing queue
    /// </summary>
    private readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

    /// <summary>
    ///     Wake up any pending dequeue task
    /// </summary>
    private TaskCompletionSource<bool> dequeueTask;
    private SemaphoreSlim @dequeueTaskLock = new SemaphoreSlim(1);
    private CancellationToken taskCancellationToken;

    internal AsyncConcurrentQueue(CancellationToken taskCancellationToken)
    {
        this.taskCancellationToken = taskCancellationToken;
    }

    /// <summary>
    ///     Supports multi-threaded producers
    /// </summary>
    /// <param name="value"></param>
    internal void Enqueue(T value)
    {
        queue.Enqueue(value);

        //signal 
        dequeueTaskLock.Wait();
        dequeueTask?.TrySetResult(true);
        dequeueTaskLock.Release();

    }

    /// <summary>
    ///     Assumes a single-threaded consumer!
    /// </summary>
    /// <returns></returns>
    internal async Task<T> DequeueAsync()
    {
        T result;
        queue.TryDequeue(out result);

        if (result != null)
        {
            return result;
        }

        await dequeueTaskLock.WaitAsync();
        dequeueTask = new TaskCompletionSource<bool>();
        dequeueTaskLock.Release();

        taskCancellationToken.Register(() => dequeueTask.TrySetCanceled());
        await dequeueTask.Task;

        queue.TryDequeue(out result);
        return result;
    }
}
