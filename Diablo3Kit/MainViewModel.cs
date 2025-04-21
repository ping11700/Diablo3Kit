
namespace Diablo3Kit;

internal class MainViewModel : NotifyObject
{
    internal MainViewModel()
    {
        //var service = new ServiceKeyboardMouse();


        GlobalVars.Config.Save();

    }

}
