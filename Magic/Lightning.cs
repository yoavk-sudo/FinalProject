namespace FinalProject.Magic
{
    internal struct Lightning
    {
        public readonly char Symbol = '╬';
        public readonly int MPCost = 8;
        public readonly int Range = 6;
        public readonly int Power = 7;
        public readonly float Speed = 0.15f; ///Speed - lower values are faster
        public readonly ConsoleColor Col = ConsoleColor.DarkCyan;
        public readonly ConsoleColor LightCol = ConsoleColor.Cyan;
        public int Charge = 2;
        public bool IsAcquired = false;
        public Lightning()
        {

        }
    }
}
