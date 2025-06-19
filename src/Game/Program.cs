using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace EmberstoneSummoner.Game;

public static class Program
{
    private static void Main()
    {
        var gameWindowSettings = GameWindowSettings.Default;
        var nativeWindowSettings = new NativeWindowSettings()
        {
            ClientSize = new Vector2i(1600, 900),
            Title = "Emberstone Summoner",
            WindowBorder = WindowBorder.Resizable
        };

        using var game = new Game(gameWindowSettings, nativeWindowSettings);
        game.Run();
    }
}