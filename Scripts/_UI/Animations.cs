using System.Collections.Generic;

namespace GodotUI
{
    public static class Animations  // unused - todo if not needed remove
    {
        private static List<string> WaitForAnimations = new List<string>();
        public static void Add(string animToken)
        {
            WaitForAnimations.Add(animToken);
        }
        public static void Remove(string animToken)
        {
            WaitForAnimations.Remove(animToken);
        }

        public static bool Wait()
        {
            return WaitForAnimations.Count > 0;
        }
    }
}