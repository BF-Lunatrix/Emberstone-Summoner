using OpenTK.Mathematics;

namespace EmberstoneSummoner.UI
{
    public interface IPanel
    {
        void Update(Vector2 mousePosition, bool mouseClicked);
        void Render();
    }
}