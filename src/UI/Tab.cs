using System;
using System.Drawing;
using OpenTK.Mathematics;

namespace EmberstoneSummoner.UI;

public class Tab
{
    public string Title;
    public Rectangle Bounds;
    public Action OnClick;
    public bool IsSelected;

    public void Render()
    {
        // Pick color based on selection
        Color4 color = IsSelected ? Color4.CornflowerBlue : Color4.Gray;

        // Draw the tab background
        PrimitiveRenderer.DrawRect(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height, color);

        // Placeholder: Draw the tab title (top-left aligned for now)
        float textX = Bounds.X + 8; // Padding
        float textY = Bounds.Y + 20; // Rough vertical offset for font height

        PrimitiveRenderer.DrawText(Title, textX, textY, Color4.White);
    }
}