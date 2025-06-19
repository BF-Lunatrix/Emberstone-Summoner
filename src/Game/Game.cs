using System;
using System.Drawing;
using EmberstoneSummoner.UI;
using EmberstoneSummoner.UI.Panels;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EmberstoneSummoner.Game;

public class Game(GameWindowSettings gameSettings, NativeWindowSettings nativeSettings)
    : GameWindow(gameSettings, nativeSettings)
{
    private const int VirtualWidth = 1600;
    private const int VirtualHeight = 900;
    private Rectangle _viewportRect;
    private UIManager _uiManager;

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(Color4.Black);

        // Initialize your renderer in VIRTUAL space:
        PrimitiveRenderer.Init(VirtualWidth, VirtualHeight);

        // Set up the UI
        _uiManager = new UIManager();
        var summonPanel = new Panel_Summon();

        _uiManager.Tabs.Add(new Tab
        {
            Title = "Summon",
            Bounds = new Rectangle(10, 10, 180, 40),
            OnClick = () =>
            {
                _uiManager.CurrentScreen = summonPanel;
                foreach (var t in _uiManager.Tabs) t.IsSelected = false;
                _uiManager.Tabs[0].IsSelected = true;
            },
            IsSelected = true
        });

        _uiManager.CurrentScreen = summonPanel;
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        // Calculate letterboxed viewport centered in the window
        float windowAspect = Size.X / (float)Size.Y;
        float targetAspect = VirtualWidth / (float)VirtualHeight;

        int vpWidth, vpHeight;
        if (windowAspect > targetAspect)
        {
            // window wider than our virtual aspect
            vpHeight = Size.Y;
            vpWidth  = (int)(vpHeight * targetAspect);
        }
        else
        {
            // window taller (or equal) than our virtual aspect
            vpWidth  = Size.X;
            vpHeight = (int)(vpWidth / targetAspect);
        }

        int vpX = (Size.X - vpWidth) / 2;
        int vpY = (Size.Y - vpHeight) / 2;

        _viewportRect = new Rectangle(vpX, vpY, vpWidth, vpHeight);

        // Set the GL viewport to this letterbox area:
        GL.Viewport(vpX, vpY, vpWidth, vpHeight);

        // Keep your projection at virtual size:
        PrimitiveRenderer.SetProjection(VirtualWidth, VirtualHeight);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        // Clear entire screen (including black bars)
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        // Draw all UI (in virtual coords clipped by our viewport)
        _uiManager.Render();

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        // Read raw mouse in window pixels
        var ms     = MouseState;
        var rawPos = ms.Position;
        bool click = ms.IsButtonDown(MouseButton.Left);

        // Convert into virtual coordinates inside the letterbox:
        var virtPos = WindowToVirtual(rawPos);

        _uiManager.Update(virtPos, click);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        PrimitiveRenderer.Cleanup();
    }

    // Converts a point in window space to virtual 1600×900 coords
    private Vector2 WindowToVirtual(Vector2 windowPos)
    {
        // subtract letterbox offset, then scale
        float vx = (windowPos.X - _viewportRect.X) * VirtualWidth  / _viewportRect.Width;
        float vy = (windowPos.Y - _viewportRect.Y) * VirtualHeight / _viewportRect.Height;
        return new Vector2(vx, vy);
    }
}