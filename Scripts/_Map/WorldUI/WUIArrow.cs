using Godot;

namespace Godot3D
{
    public partial class WUIArrow : Node3D
    {
        private Node3D _arrowModel;

        public override void _Ready()
        {
            _arrowModel = GetNode<Node3D>("ArrowModel");
        }

        public void Refresh(Data.HexCoord atHexCoord, Data.HexCoord pointToHexCoord)
        {
            Vector3 position = Convert.HexCoordToWorld(atHexCoord);
            Vector3 pointToPosition = Convert.HexCoordToWorld(pointToHexCoord);
            Position = position;
            Rotation = new Vector3(0.0f, -(pointToPosition - position).SignedAngleTo(Vector3.Forward, Vector3.Up), 0.0f);
        }
    }
}
