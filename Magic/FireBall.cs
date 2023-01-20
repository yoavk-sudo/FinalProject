namespace FinalProject.Magic
{
    internal struct FireBall //struct?
    {
        // make stuff const?
        readonly public int MPCost = 1;
        readonly public char Symbol = 'o';
        public int Charge = 2;
        readonly public int Range = 4;
        readonly public int Power = 3;
        readonly public float Speed = 0.65f; ///?
        public bool IsAcquired = false;
        readonly public ConsoleColor Col = ConsoleColor.DarkRed;
        readonly public ConsoleColor LightCol = ConsoleColor.Red;
        public FireBall()
        {

        }
    }
}
