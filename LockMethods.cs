namespace FinalProject
{
    internal struct LockMethods
    {
        static public readonly object ActionLock = new object();
        static public void SetCursorLockAndOneSpace(int[] coordinates, int x, int y)
        {
            lock (ActionLock)
            {
                Console.SetCursorPosition(coordinates[0] + x, coordinates[1] + y);
                Console.Write(' ');
            }
        }
    }
}
