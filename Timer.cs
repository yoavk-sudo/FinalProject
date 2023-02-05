namespace FinalProject
{
    internal static class Timer
    {
        public static bool IsTimerActive = false;
        static int _time = 0;
        const int STARTINGPOS = 40;
        public static async Task AsyncTimer()
        {
            while (true)
            {
                lock (LockMethods.ActionLock)
                {
                    Console.SetCursorPosition(STARTINGPOS, 0);
                    Console.WriteLine("Time: " + _time);
                    _time++;
                }
                await Task.Delay(1000);
            }
        }
    }
}
