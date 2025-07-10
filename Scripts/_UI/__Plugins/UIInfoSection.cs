using Godot;
using Godot.Collections;
using System.Collections.Generic;

namespace GodotUI
{
    public partial class UIInfoSection : VBoxContainer
    {
        public struct Texts
        {
            public string Title;
            public string Description;

            public Texts()
            {
                Title = "";
                Description = "";
            }
        }

        private Array<UIInfoBlock> _UIInfoBlocks = new Array<UIInfoBlock>();

        public override void _Ready()
        {
            _UIInfoBlocks.Add(GetNode<UIInfoBlock>("InfoBlock_0"));
        }

        public void SetTexts(List<Texts> titlesAndDescriptions)
        {
            // grow
            while (_UIInfoBlocks.Count < titlesAndDescriptions.Count)
            {
                UIInfoBlock newItem = _UIInfoBlocks[0].Duplicate(7) as UIInfoBlock;
                _UIInfoBlocks[0].GetParent().AddChild(newItem);
                _UIInfoBlocks.Add(newItem);
            }

            for (int idx = 0; idx < _UIInfoBlocks.Count; idx++)
            {
                if (idx < titlesAndDescriptions.Count)
                {
                    _UIInfoBlocks[idx].Visible = true;
                    _UIInfoBlocks[idx].SetTexts(titlesAndDescriptions[idx].Title, titlesAndDescriptions[idx].Description);
                }
                else
                {
                    _UIInfoBlocks[idx].Visible = false;
                }
            }
        }
    }
}