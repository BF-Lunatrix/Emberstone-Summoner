using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace EmberstoneSummoner;

public class Game(GameWindowSettings gameSettings, NativeWindowSettings nativeSettings)
    : GameWindow(gameSettings, nativeSettings)
{
    private const int VirtualWidth = 1600;
    private const int VirtualHeight = 900;
    private Rectangle _viewportRect;

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(Color4.Black);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        float windowAspect = Size.X / (float)Size.Y;
        float targetAspect = VirtualWidth / (float)VirtualHeight;

        int viewportWidth, viewportHeight;

        if (windowAspect > targetAspect)
        {
            viewportHeight = Size.Y;
            viewportWidth = (int)(viewportHeight * targetAspect);
        }
        else
        {
            viewportWidth = Size.X;
            viewportHeight = (int)(viewportWidth / targetAspect);
        }

        int viewportX = (Size.X - viewportWidth) / 2;
        int viewportY = (Size.Y - viewportHeight) / 2;

        GL.Viewport(viewportX, viewportY, viewportWidth, viewportHeight);
        _viewportRect = new Rectangle(viewportX, viewportY, viewportWidth, viewportHeight);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        // TODO: Add rendering here (UI, sprites, etc.)

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        // TODO: Add input handling, ECS, logic updates
    }
}