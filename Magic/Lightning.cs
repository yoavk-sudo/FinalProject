namespace FinalProject.Magic
{
    internal struct Lightning
    {
        readonly public int MPCost = 3;
        readonly public char Symbol = '╬';
        public int Charge = 2;
        readonly public int Range = 6;
        readonly public int Power = 3;
        readonly public float Speed = 0.15f; ///?
        public bool IsAcquired = false;
        readonly public ConsoleColor Col = ConsoleColor.DarkCyan;
        readonly public ConsoleColor LightCol = ConsoleColor.Cyan;
        public Lightning()
        {

        }
    }
}
