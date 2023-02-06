namespace FinalProject.Magic
{
    internal struct Teleport //struct?
    {
        // make stuff const?
        readonly public int MPCost = 2;
        readonly public char Symbol = '§';
        public int Charge = 2;
        readonly public static int Range = 5;
        readonly public int Power = 0;
        readonly public float Speed = 0;
        public bool IsAcquired = false;
        readonly public ConsoleColor Col = ConsoleColor.DarkYellow;
        readonly public ConsoleColor LightCol = ConsoleColor.Yellow;
        public Teleport()
        {

        }
    }
}
