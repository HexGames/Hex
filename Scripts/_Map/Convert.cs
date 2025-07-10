using Godot;

namespace Godot3D
{
    public static class Convert
    {
        public static Vector3 HexCoordToWorld(Data.HexCoord coords)
        {
            float x = Base.HEX_RADIUS * Mathf.Sqrt(3) * (coords.X + coords.Y / 2.0f);
            float z = Base.HEX_RADIUS * 3.0f / 2.0f * coords.Y;
            return new Vector3(x, 0, z);
        }
        public static Data.HexCoord FromWorldXZ(Vector3 position)
        {
            float x = position.X;
            float z = position.Z;

            // Convert world (x, z) to axial coordinates (q, r)
            float hexX = (Mathf.Sqrt(3f) / 3f * x - 1f / 3f * z) / Base.HEX_RADIUS;
            float hexY = (2f / 3f * z) / Base.HEX_RADIUS;

            // Convert to cube coordinates
            float cubeX = hexX;
            float cubeY = hexY;
            float cubeZ = -cubeX - cubeY;

            // Round to nearest cube coordinates
            int rx = Mathf.RoundToInt(cubeX);
            int ry = Mathf.RoundToInt(cubeY);
            int rz = Mathf.RoundToInt(cubeZ);

            float x_diff = Mathf.Abs(rx - cubeX);
            float y_diff = Mathf.Abs(ry - cubeY);
            float z_diff = Mathf.Abs(rz - cubeZ);

            if (x_diff > y_diff && x_diff > z_diff)
                rx = -ry - rz;
            else if (y_diff > z_diff)
                ry = -rx - rz;
            // else rz = -rx - ry; // Not needed for axial output

            // Return as axial (q, r) = (rx, ry)
            return new Data.HexCoord(rx, ry);
        }
    }
}
