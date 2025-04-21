namespace Diablo3Kit.Hook;

/// <summary>
///     Wraps low level keyboard hook.
///     Uses a producer-consumer pattern to improve performance and to avoid operating system forcing unhook on delayed
///     user callbacks.
/// </summary>
public class KeyboardWatcher
{
    private readonly object accesslock = new();

    private readonly SyncFactory factory;

    private KeyboardHook keyboardHook;
    private AsyncConcurrentQueue<object> keyQueue;
    private CancellationTokenSource taskCancellationTokenSource;

    internal KeyboardWatcher(SyncFactory factory)
    {
        this.factory = factory;
    }

    private bool isRunning { get; set; }
    public event EventHandler<KeyInputEventArgs> OnKeyInput;

    /// <summary>
    ///     Start watching
    /// </summary>
    public void Start()
    {
        lock (accesslock)
        {
            if (!isRunning)
            {
                taskCancellationTokenSource = new CancellationTokenSource();
                keyQueue = new AsyncConcurrentQueue<object>(taskCancellationTokenSource.Token);

                //This needs to run on UI thread context
                //So use task factory with the shared UI message pump thread
                Task.Factory.StartNew(() =>
                {
                    keyboardHook = new KeyboardHook();

                    keyboardHook.KeyDown -= KListener;
                    keyboardHook.KeyDown += KListener;
                    keyboardHook.KeyUp -= KListener;
                    keyboardHook.KeyUp += KListener;
                    keyboardHook.Start();
                },
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    factory.GetTaskScheduler()).Wait();

                Task.Factory.StartNew(() => ConsumeKeyAsync());

                isRunning = true;
            }
        }
    }

    /// <summary>
    ///     Stop watching
    /// </summary>
    public void Stop()
    {
        lock (accesslock)
        {
            if (isRunning)
            {
                if (keyboardHook != null)
                {
                    //This needs to run on UI thread context
                    //So use task factory with the shared UI message pump thread
                    Task.Factory.StartNew(() =>
                    {
                        keyboardHook.KeyDown -= KListener;
                        keyboardHook.Stop();
                        keyboardHook = null;
                    },
                        CancellationToken.None,
                        TaskCreationOptions.None,
                        factory.GetTaskScheduler());
                }

                keyQueue.Enqueue(false);
                isRunning = false;
                taskCancellationTokenSource.Cancel();
            }
        }
    }

    /// <summary>
    ///     Add key event to the producer queue
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void KListener(object sender, RawKeyEventArgs e)
    {
        keyQueue.Enqueue(new KeyData
        {
            UnicodeCharacter = e.Character,
            Keyname = e.Key.ToString(),
            EventType = (KeyEvent)e.EventType
        });
    }

    /// <summary>
    ///     Consume events from the producer queue asynchronously
    /// </summary>
    /// <returns></returns>
    private async Task ConsumeKeyAsync()
    {
        while (isRunning)
        {
            //blocking here until a key is added to the queue
            var item = await keyQueue.DequeueAsync();

            if (item is null)
            {
                continue;
            }

            if (item is bool)
            {
                break;
            }

            KListener_KeyDown((KeyData)item);
        }
    }

    /// <summary>
    ///     Invoke user call backs
    /// </summary>
    /// <param name="kd"></param>
    private void KListener_KeyDown(KeyData kd)
    {
        OnKeyInput?.Invoke(null, new KeyInputEventArgs { KeyData = kd });
    }
}