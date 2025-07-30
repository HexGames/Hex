using Godot;

namespace GodotUI
{
    public partial class UIInfoBlock : VBoxContainer
    {
        private UIText _Title;
        private UIText _Description;

        public override void _Ready()
        {
            _Title = GetNode<UIText>("Title");
            _Description = GetNode<UIText>("Description");
        }

        public void SetTexts(string title, string description)
        {
            _Title.SetText("$title", title);
            _Description.SetText("$description", description);
        }
    }
}