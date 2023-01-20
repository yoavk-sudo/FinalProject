using FinalProject.Elements;

namespace FinalProject
{
    internal class Enemy
    {
        readonly string _type;
        readonly int _maxHP;
        int _currentHP;
        readonly int _power;
        int _diff; // 0 or 1 = easy or hard
        readonly int _gold;
        int _exp;
        static List<Enemy> enemies = new List<Enemy>();
        //Constructor
        public Enemy(string type, int maxHP, int power)
        {
            enemies.Add(this);
            _type = type;
            _maxHP = maxHP;
            _currentHP = _maxHP;
            _power = power;
            _gold = GoldAmount(power);
            EnemyList.AddToList(this);
        }
        public static Enemy CreateEnemy() ///////////////////////////////////////////
        {
            string eType = "1";
            int hp = 1;
            int pow = 1;
            Enemy enemy = new Enemy(eType, hp, pow);
            return enemy;
        }
        //Properties
        public int[] Coordinates
        {
            get; set;
        } = { 0, 0 };
        public int HP
        {
            get { return _currentHP; }
            set { _currentHP = value; }
        }
        public int EXP { get { return _exp; } set { _exp = value; } }
        //Passive qualities
        private int GoldAmount(int power)
        {
            int min = power * 2;
            int max = power * 3;
            int amount = Random.Shared.Next(min, max);
            return amount;
        }
        public bool IsAlive()
        {
            if (this.HP <= 0)
            {
                enemies.Remove(this);
                return false;
            }
            return true;
        }
        public bool WithinMoveRange(Player player)
        {
            return CalculateDistanceToPlayer(player) <= 3 && CalculateDistanceToPlayer(player) > 1;
        }
        //Passive Actions
        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP < 0)
            {
                HP = 0;
            }
        }
        public void DealDamage(Player player)
        {
            player.TakeDamage(_power);
        }
        public int CalculateDistanceToPlayer(Player player)
        {
            int xDistance, yDistance;
            xDistance = this.Coordinates[0] - player.Coordinates[0];
            yDistance = this.Coordinates[1] - player.Coordinates[1];
            if (xDistance == 1 || yDistance == 1) return 1;
            if (xDistance > yDistance) return xDistance;
            return yDistance;
            //return xDistance + yDistance;
        }
        public int CalculateDistanceToPlayer(Player player, int x, int y)
        {
            int xDistance, yDistance;
            xDistance = Math.Abs(this.Coordinates[0] - player.Coordinates[0] + x);
            yDistance = Math.Abs(this.Coordinates[1] - player.Coordinates[1] + y);
            return xDistance + yDistance;
        }
        //Actions
        public void Move(Player player)
        {
            if (!WithinMoveRange(player)) return;
            int up = CalculateDistanceToPlayer(player, 0, -1);
            int right = CalculateDistanceToPlayer(player, 1, 0);
            int down = CalculateDistanceToPlayer(player, 0, 1);
            int left = CalculateDistanceToPlayer(player, -1, 0);
            int yAxis = Math.Min(up, down);
            int xAxis = Math.Min(right, left);
            int finalDirection = Math.Min(xAxis, yAxis);
        }
        public void Move()
        {
            while (true)
            {
                Console.SetCursorPosition(Coordinates[0], Coordinates[1]);
                Console.Write(' ');
                Coordinates[1] += 1;
                Console.SetCursorPosition(Coordinates[0], Coordinates[1]);
                Console.Write('¤');
                Thread.Sleep(1000);
            }
        }
        public async void StartAsyncTask()
        {
            // Start the task.
            await Task.Run(() => Move());
        }
    }
}
