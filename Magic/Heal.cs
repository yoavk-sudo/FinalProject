namespace FinalProject.Magic
{
    internal struct Heal
    {
        readonly public int MPCost = 1;
        readonly public char Symbol = '÷';
        public int Charge = 2;
        readonly public int Range = 0;
        readonly public static int Power = 3;
        readonly public float Speed = 0;
        public bool IsAcquired = false;
        readonly public ConsoleColor Col = ConsoleColor.DarkGreen;
        readonly public ConsoleColor LightCol = ConsoleColor.Green;
        public Heal()
        {

        }
    }
}
