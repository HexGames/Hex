namespace Hex.Data
{
    public static class Unused_DataTester // just for testing data - if it compiles than all data are continous memory blocks (blittable types)
    {
        private static void EnsureUnmanaged<T>() where T : unmanaged { }
        internal static void InitData()
        {
            // Check that the struct is an unbroken chunck of memory
            EnsureUnmanaged<Benefit>();
            EnsureUnmanaged<HexPos>();
            EnsureUnmanaged<Res>();
            EnsureUnmanaged<Tile>();
            EnsureUnmanaged<Turn>();
            EnsureUnmanaged<GameData>();
        }
    }
}
