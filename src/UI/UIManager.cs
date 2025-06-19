using OpenTK.Mathematics;

namespace EmberstoneSummoner.UI
{
    public class UIManager
    {
        public List<Tab> Tabs = new();
        public IPanel CurrentScreen;

        public void Update(Vector2 mousePosition, bool mouseClicked)
        {
            foreach (var tab in Tabs)
            {
                if (mouseClicked && tab.Bounds.Contains((int)mousePosition.X, (int)mousePosition.Y))
                    tab.OnClick?.Invoke();
            }

            CurrentScreen?.Update(mousePosition, mouseClicked);
        }

        public void Render()
        {
            foreach (var tab in Tabs)
                tab.Render();

            CurrentScreen?.Render();
        }
    }
}