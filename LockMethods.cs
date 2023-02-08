namespace FinalProject
{
    internal struct LockMethods
    {
        public static readonly object ActionLock = new object();
        public static void SetCursorLockAndOneSpace(int[] coordinates, int x = 0, int y = 0)
        {
            lock (ActionLock)
            {
                if (coordinates == null) return;
                Console.SetCursorPosition(coordinates[0] + x, coordinates[1] + y);
                Console.Write(' ');
            }
        }
    }
}
