namespace FinalProject.Magic
{
    internal struct Heal
    {
        public readonly char Symbol = '+';
        public readonly int MPCost = 5;
        public readonly int Range = 0;
        public readonly static int Power = 5;
        public readonly float Speed = 0; ///Speed - lower values are faster
        public readonly ConsoleColor Col = ConsoleColor.DarkGreen;
        public readonly ConsoleColor LightCol = ConsoleColor.Green;
        public int Charge = 2;
        public bool IsAcquired = false;
        public Heal()
        {

        }
    }
}
