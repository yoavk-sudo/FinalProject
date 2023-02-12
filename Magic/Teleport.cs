namespace FinalProject.Magic
{
    internal struct Teleport //struct?
    {
        public readonly char Symbol = '§';
        public readonly int MPCost = 1;
        public readonly int Power = 0;
        public readonly float Speed = 0; ///Speed - lower values are faster
        public readonly static int Range = 5;
        public readonly ConsoleColor Col = ConsoleColor.DarkYellow;
        public readonly ConsoleColor LightCol = ConsoleColor.Yellow;
        public int Charge = 2;
        public bool IsAcquired = false;
        public Teleport()
        {

        }
    }
}
