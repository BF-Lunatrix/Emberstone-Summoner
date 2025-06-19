using OpenTK.Mathematics;

namespace EmberstoneSummoner.UI.Panels
{
    public class Panel_Summon : IPanel
    {
        public void Update(Vector2 mousePos, bool click)
        {
            // Later: respond to clicks, e.g. summoning button
        }

        public void Render()
        {
            // Placeholder render logic
            PrimitiveRenderer.DrawRect(200, 50, 800, 600, Color4.DarkSlateGray);
            PrimitiveRenderer.DrawText("Summon Panel", 220, 70, Color4.White);
        }
    }
}
