using Godot;
using Godot.Collections;
using System.ComponentModel;

namespace GodotUI
{
    public partial class UIText : RichTextLabel
    {
        [ExportGroup("Runtime")]
        [Export] // <-- needs to be serialized
        private string Original = "";
        private UITooltipTrigger ToolTip = null;

        public override void _Ready()
        {
            if (Original == "") Original = Text;

            if (HasNode("ToolTip")) ToolTip = GetNode<UITooltipTrigger>("ToolTip");
            if (ToolTip == null && HasNode("../ToolTip")) ToolTip = GetNode<UITooltipTrigger>("../ToolTip");
        }

        public void SetText(string replace_1, string withText_1)
        {
            Text = Original.Replace(replace_1, withText_1).Replace("@", "\n").Replace("_", " ");
        }

        public void SetText(string replace_1, string withText_1, string replace_2, string withText_2)
        {
            Text = Original.Replace(replace_1, withText_1).Replace(replace_2, withText_2).Replace("@", "\n").Replace("_", " ");
        }

        public void SetText(string replace_1, string withText_1, string replace_2, string withText_2, string replace_3, string withText_3)
        {
            Text = Original.Replace(replace_1, withText_1).Replace(replace_2, withText_2).Replace(replace_3, withText_3).Replace("@", "\n").Replace("_", " ");
        }
    }
}