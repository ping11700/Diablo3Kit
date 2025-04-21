namespace Diablo3Kit.Data;

internal class GlobalVars
{
    static GlobalVars()
    {
        Config = new();
        Config.Load();
    }


    internal static Config Config { get; set; }

}
