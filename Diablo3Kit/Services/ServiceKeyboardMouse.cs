namespace Diablo3Kit.Service;

internal class ServiceKeyboardMouse:IDisposable
{

    private KeyboardWatcher _keyboardWatcher;
    private CancellationTokenSource _cts;
    private bool _tag;

    internal ServiceKeyboardMouse()
    {
        _keyboardWatcher = new KeyboardWatcher(new SyncFactory());
        _cts=new CancellationTokenSource();

        _keyboardWatcher.OnKeyInput -= OnKeyInput;
        _keyboardWatcher.OnKeyInput += OnKeyInput;
        _keyboardWatcher.Start();

    }


    private void OnKeyInput(object sender, KeyInputEventArgs e)
    {
        if (e.KeyData.EventType == KeyEvent.down)
        {
            if (e.KeyData.Keyname == "Oem3")
            {
                if (_tag == false)
                {
                    Simulate();
                    _tag = true;
                }
                else
                {
                    _tag = false;
                    _cts.Cancel();
                    _cts.Dispose();
                    _cts = new CancellationTokenSource();
                }
            } 

        }


    }



    private void Simulate()
    {
        //Mouse
        Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                _cts.Token.ThrowIfCancellationRequested();
                try
                {
                    SendMouseClick();
                    await Task.Delay((new Random()).Next(400, 600), _cts.Token);
                }
                catch (TaskCanceledException ex) { }
            }
        }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();



        Process[] processes = Process.GetProcessesByName("Diablo III64");
        var winHandle = processes[0].MainWindowHandle;


        //key
        Task.Factory.StartNew(async () =>
        {
            while (true)
            {
                _cts.Token.ThrowIfCancellationRequested();
                try
                {
                    await Task.Delay((new Random()).Next(500, 800), _cts.Token);
                    SendKey(winHandle, VirtualKeyCode.VK_Q);
                }
                catch (TaskCanceledException ex) { }
            }
        }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();


    }








    public void Dispose()
    {
        _keyboardWatcher.OnKeyInput -= OnKeyInput;
        _keyboardWatcher.Stop();

        _keyboardWatcher = null;
    }

     




    private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
    private const uint MOUSEEVENTF_LEFTUP = 0x04;
    private void SendMouseClick()
    {
        //IntPtr lParam = (IntPtr)((y << 16) | x);

        //NativeAPI.SendMessage(handle, (uint)WindowMessage.LBUTTONDOWN, 0, lParam);
        //NativeAPI.SendMessage(handle, (uint)WindowMessage.LBUTTONUP, 0, lParam);


        NativeAPI.GetCursorPos(out var point);
        NativeAPI.mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, IntPtr.Zero);
    }


    private const uint KEYDOWN = 0x0100;
    private const uint CHAR = 0x0102;
    private const uint KEYUP = 0x0101;
    private void SendKey(IntPtr handle, VirtualKeyCode keyCode)
    {
        NativeAPI.SendMessage(handle, KEYDOWN, (int)keyCode, IntPtr.Zero);
        NativeAPI.SendMessage(handle, CHAR, 0x01, IntPtr.Zero);
        NativeAPI.SendMessage(handle, KEYUP, (int)keyCode, IntPtr.Zero);
    }


}
