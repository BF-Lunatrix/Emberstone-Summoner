using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using EmberstoneSummoner.UI.Shaders;

namespace EmberstoneSummoner.UI;

public static class PrimitiveRenderer
{
    private static Shader _shader;
    private static int _vao;
    private static int _vbo;

    private static Matrix4 _projection;

    private static bool _initialized;

    public static void Init(int screenWidth, int screenHeight)
    {
        if (_initialized)
            return;

        _shader = new Shader("Shaders/ui.vert", "Shaders/ui.frag");

        // Rectangle: two triangles forming a quad
        _vbo = GL.GenBuffer();
        _vao = GL.GenVertexArray();

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);

        // Each vertex: x, y (2 floats = 8 bytes per vertex)
        GL.BufferData(BufferTarget.ArrayBuffer, 4 * 2 * sizeof(float), IntPtr.Zero, BufferUsageHint.DynamicDraw);

        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);

        SetProjection(screenWidth, screenHeight);

        _initialized = true;
    }

    public static void SetProjection(int width, int height)
    {
        _projection = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, -1, 1);
    }

    public static void DrawRect(float x, float y, float width, float height, Color4 color)
    {
        if (!_initialized)
            throw new Exception("PrimitiveRenderer not initialized. Call Init() first.");

        float[] vertices = {
            x,         y,
            x + width, y,
            x + width, y + height,
            x,         y + height
        };

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Length * sizeof(float), vertices);

        _shader.Use();

        // Set color
        int locColor = GL.GetUniformLocation(_shader.Handle, "uColor");
        if (locColor != -1)
            GL.Uniform4(locColor, color);

        // Set projection matrix
        int locProj = GL.GetUniformLocation(_shader.Handle, "uProjection");
        if (locProj != -1)
            GL.UniformMatrix4(locProj, false, ref _projection);

        // Draw the quad as a triangle fan
        GL.DrawArrays(PrimitiveType.TriangleFan, 0, 4);

        GL.BindVertexArray(0);
    }
    
    public static void DrawText(string text, float x, float y, Color4 color)
    {
        // This is a stub! For now, do nothing
    }

    public static void Cleanup()
    {
        if (_initialized)
        {
            GL.DeleteVertexArray(_vao);
            GL.DeleteBuffer(_vbo);
            _shader.Dispose();
            _initialized = false;
        }
    }
}