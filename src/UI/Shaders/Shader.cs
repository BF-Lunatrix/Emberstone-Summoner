using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace EmberstoneSummoner.UI.Shaders;

public class Shader : IDisposable
{
    public int Handle { get; private set; }

    public Shader(string vertexPath, string fragmentPath)
    {
        string vertexCode = File.ReadAllText(vertexPath);
        string fragmentCode = File.ReadAllText(fragmentPath);

        int vertexShader = CompileShader(ShaderType.VertexShader, vertexCode);
        int fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentCode);

        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vertexShader);
        GL.AttachShader(Handle, fragmentShader);
        GL.LinkProgram(Handle);

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            throw new Exception($"Shader linking failed: {infoLog}");
        }

        GL.DetachShader(Handle, vertexShader);
        GL.DetachShader(Handle, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    private int CompileShader(ShaderType type, string source)
    {
        int shader = GL.CreateShader(type);
        GL.ShaderSource(shader, source);
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(shader);
            throw new Exception($"{type} compilation failed: {infoLog}");
        }

        return shader;
    }

    public void Use() => GL.UseProgram(Handle);

    public void SetInt(string name, int value)
    {
        int location = GL.GetUniformLocation(Handle, name);
        if (location != -1)
            GL.Uniform1(location, value);
    }

    public void Dispose()
    {
        GL.DeleteProgram(Handle);
    }
}