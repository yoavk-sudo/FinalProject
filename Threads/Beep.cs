namespace FinalProject.Threads
{
    internal static class Beep
    {
        public static Thread beepThread = new Thread(sdf);
        
        static Beep()
        {
            beepThread.Name = "Beep";          
        }

        static public void sdf()
        {
            Console.Beep(105,100);
        }
    }
}
