using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Hex.GodotMap
{
    public partial class CursorTileNode : Node3D
    {
        private Viewport _viewport;
        private Camera3D _camera;

        private Data.HexPos _lastHexPos = Data.HexPos.INVALID;
        private Node3D _instance = null;

        private Data.DeckTileRef _currentDeckTile = Data.DeckTileRef.INVALID;

        public override void _Ready()
        {
            _viewport = GetViewport();
            _camera = _viewport.GetCamera3D();

            Visible = false;
        }

        public void Activate(Data.DeckTileRef deckTile)
        {
            Cleanup();

            _currentDeckTile = deckTile;

            var prefab = Main.x.Assets.GetPrefab_Tiles(_currentDeckTile.Value.Def.Map_TilePrefab);
            if (prefab != null)
            {
                _instance = prefab.Instantiate<Node3D>();
                _instance.Position = Vector3.Up;
                AddChild(_instance);
            }
        }

        public void Clear()
        {
            Cleanup();

            _currentDeckTile = Data.DeckTileRef.INVALID;

            Visible = false;
        }

        private void Cleanup()
        {
            if (_instance == null)
                return;

            _instance.QueueFree();
            _instance = null;
        }

        public override void _Process(double delta)
        {
            if (_instance == null)
                return;

            // Get the mouse position in the viewport
            Vector2 mousePos = _viewport.GetMousePosition();

            // Cast a ray from the camera through the mouse position into the xoz plane (y = 0)
            Vector3 from = _camera.ProjectRayOrigin(mousePos);
            Vector3 dir = _camera.ProjectRayNormal(mousePos);
            if (dir.Y == 0)
                return; // Parallel to the plane, no intersection

            // Calculate intersection with y = 0 plane
            float t = -from.Y / dir.Y;
            Vector3 intersection = from + dir * t;

            Data.HexPos hexPos = Convert.WorldToHexPos(intersection);

            if (Hex.Data.MapHelper.IsHexPosOnMap(hexPos))
            {
                if (_lastHexPos == hexPos) return; // no change
            }
            else
            {
                if (_lastHexPos == Data.HexPos.INVALID) return; // no change

                hexPos = Data.HexPos.INVALID;
            }

            OnHoverChange(_currentDeckTile, hexPos);
            _lastHexPos = hexPos;
        }

        public override void _Input(InputEvent @event)
        {
            if (_instance == null)
                return;

            if (@event is InputEventMouseButton mouseEvent)
            {
                if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
                {
                    OnLeftClick();
                }
            }
        }


        // ---------------------------------------------------------------------------------------------------
        private void OnLeftClick()
        {
            Hex.Play.Game.InputPlayTile(_lastHexPos);
        }


        internal static List<Data.Benefit> OnHoverBenefits = new List<Data.Benefit>(4);
        internal static int OnHoverBenefitsCount;
        internal static void ClearOnHoverBenefits()
        {
            OnHoverBenefitsCount = 0;
            for (int idx = 0; idx < OnHoverBenefits.Count; idx++) OnHoverBenefits[idx] = default;
        }

        internal static void AddOnHoverBenefit(Def.ResRef def, int value, Data.HexPos hexPos, Def.Timing benefitTiming)
        {
            Span<Data.Benefit> benefits = CollectionsMarshal.AsSpan(OnHoverBenefits).Slice(0, OnHoverBenefitsCount);
            for (int idx = 0; idx < benefits.Length; idx++)
            {
                if (benefits[idx].Res.Def == def && benefits[idx].HexPos == hexPos && benefits[idx].BenefitTiming == benefitTiming)
                {
                    benefits[idx].Res.Value += value;
                    return;
                }
            }

            if (OnHoverBenefitsCount >= OnHoverBenefits.Count)
            {
                OnHoverBenefits.Add(new Data.Benefit(def, value, hexPos, benefitTiming));
            }
            else
            {
                OnHoverBenefits[OnHoverBenefitsCount] = new Data.Benefit(def, value, hexPos, benefitTiming);
            }
            OnHoverBenefitsCount++;
        }

        private void OnHoverChange(Data.DeckTileRef deckTile, Data.HexPos hexPos)
        {
            if (hexPos != Data.HexPos.INVALID)
            {
                // 3d Update - general
                Visible = true;
                Position = Convert.HexPosToWorld(hexPos);

                // UI update - general
                UI.TileInfo.Show();

                // simulate placement
                bool success = Logic.Actions.PlayTile(deckTile, hexPos, out Data.MapTileRef mapTile, out ReadOnlySpan<Logic.Production> onPlaceProduction);
                if (success)
                {
                    // valid position
                    Logic.Actions.EndTurn(out ReadOnlySpan<Logic.Production> OnEndTurnProduction);

                    ClearOnHoverBenefits();
                    for (int idx = 0; idx < onPlaceProduction.Length; idx++)
                    {
                        AddOnHoverBenefit(onPlaceProduction[idx].Total.Def, onPlaceProduction[idx].Total.Value, hexPos, Def.Timing.OnPlace);
                    }
                    for (int idx = 0; idx < OnEndTurnProduction.Length; idx++)
                    {
                        AddOnHoverBenefit(OnEndTurnProduction[idx].Total.Def, OnEndTurnProduction[idx].Total.Value, hexPos, Def.Timing.OnPlace);
                    }

                    // 3d Update
                    _instance.Visible = true;

                    //UI update
                    Span<Data.Benefit> benefits = CollectionsMarshal.AsSpan(OnHoverBenefits).Slice(0, OnHoverBenefitsCount);
                    UI.TileInfo.RefrehsForBenefits(deckTile, benefits);

                    // undo EndTurn and PlayTile
                    Logic.Actions.UndoTurn();
                }
                else
                {
                    // invalid position

                    // 3d Update
                    _instance.Visible = false;

                    //UI update
                    UI.TileInfo.RefrehsForEffects(_currentDeckTile);
                }


                GodotUI.UIMain.X.DebugText.SetText("$", $"{hexPos}");
            }
            else
            {
                // 3d Update
                Visible = false;

                //UI update
                UI.TileInfo.Hide();
            }
        }

        private void OnRightClick()
        {

        }
    }
}
