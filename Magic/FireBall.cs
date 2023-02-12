namespace FinalProject.Magic
{
    internal struct FireBall //struct?
    {
        public readonly char Symbol = 'o';
        public readonly int MPCost = 2;
        public readonly int Range = 4;
        public readonly int Power = 4;
        public readonly float Speed = 0.55f; ///Speed - lower values are faster
        public readonly ConsoleColor Col = ConsoleColor.DarkRed;
        public readonly ConsoleColor LightCol = ConsoleColor.Red;
        public int Charge = 2;
        public bool IsAcquired = false;
        public FireBall()
        {

        }
    }
}
