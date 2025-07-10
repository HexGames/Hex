using Godot;

namespace GodotUI
{
    public partial class UITooltipTile : Control
    {
        // is beeing duplicated... maybe
        private UIText Title = null;
        private UIText Type = null;
        private UIText Benefit = null;
        private UIText Description = null;

        public bool CanBeHovered = true;

        private bool IsTargetHovered = false;
        private bool IsTooltipHovered = false;

        private float TimeVisible = 0.0f;

        public override void _Ready()
        {
            if (Engine.IsEditorHint()) return;

            Title = GetNode<UIText>("VBoxContainer/Text");
            Type = GetNode<UIText>("VBoxContainer/Type");
            Benefit = GetNode<UIText>("VBoxContainer/VBoxContainer/Benefits");
            Description = GetNode<UIText>("VBoxContainer/Description");
        }

        public void Refresh(Data.Tile tile)
        {
            Title.SetText("$", tile.Def.UI_ToolTip_Title);
            Description.SetText("$", tile.Def.UI_ToolTip_Description);

            Type.SetText("$", "Tile");

            string benefitText = "";
            //foreach (var benefit in tile.Benefits)
            //{
            //    benefitText += benefitText.Length > 0 ? "\n" : "";
            //    benefitText += "Gain " + benefit.Value.ToString() + UIHelper.GetIcon(benefit.Def.Image, 32) + benefit.Def.Title;
            //    if (benefit.Turns > 0) benefitText += " in " + benefit.Turns.ToString() + UIHelper.GetIcon("Turn", 32) + "Turns";
            //}

            //if (card.Building != null)
            //{
            //    //benefitText += "Build " + (card.Building.Count > 1 ? card.Building.Count.ToString() : "a") + " " + card.Building.Def.UI_Title;
            //    benefitText = "";
            //    foreach (var benefit in card.Building.Resources)
            //    {
            //        benefitText += benefitText.Length > 0 ? "\n" : "";
            //        if (benefit.PerTurn > 0)
            //        {
            //            benefitText += "Gain +" + benefit.PerTurn.ToString() + Helper.GetIcon(benefit.ResDef.Image, 32) + benefit.ResDef.Title + " each " + Helper.GetIcon("Turn", 32) + "Turn";
            //        }
            //        else
            //        {
            //            benefitText += "Gain " + benefit.Value.ToString() + Helper.GetIcon(benefit.ResDef.Image, 32) + benefit.ResDef.Title;
            //        }
            //    }
            //}
            Benefit.SetText("$", benefitText);
        }

        public void OnHoverEnter()
        {
            IsTooltipHovered = CanBeHovered;
        }

        public void OnHoverExit()
        {
            IsTooltipHovered = false;
        }
        public void TargetHoverEnter()
        {
            IsTargetHovered = true;
        }

        public void TargetHoverExit()
        {
            IsTargetHovered = false;
        }
        public override void _Process(double delta)
        {
            if (IsTargetHovered || IsTooltipHovered)
            {
                TimeVisible += (float)delta;
            }
            else
            {
                TimeVisible = 0.0f;
            }
            Visible = TimeVisible >= 0.35f;
        }

        //public void SetVisible()
        //{
        //    Visible = true;
        //}
    }
}