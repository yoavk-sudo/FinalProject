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
        const int MAXRANGE = 5;
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
        
        private void LockEnemyMoveCoordinates(int dirX, int dirY)
        {
            if (Map.WhatIsInNextTile(Coordinates, new int[]{ dirX, dirY }) != 0) return;
            LockMethods.SetCursorLockAndOneSpace(Coordinates); //if not obstructed
            Coordinates[0] += dirX;
            Coordinates[1] += dirY;
            lock (LockMethods.ActionLock)
            {
                Console.SetCursorPosition(Coordinates[0], Coordinates[1]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('¤');
                Console.ResetColor();
            }
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
            //player.TakeDamage(_power);
        }
        public bool WithinMoveRange(Player player)
        {
            return CalculateDistanceToPlayer(player) <= MAXRANGE && CalculateDistanceToPlayer(player) >= 0;
        }
        public int CalculateDistanceToPlayer(Player player)
        {
            int xDistance = Math.Abs(this.Coordinates[0] - player.Coordinates[0]);
            int yDistance = Math.Abs(this.Coordinates[1] - player.Coordinates[1]);
            if (xDistance < yDistance) return xDistance;
            return yDistance;
        }
        public int CalculateDistanceToPlayer(Player player, int dirX, int dirY)
        {
            int xDistance = Math.Abs(this.Coordinates[0] - player.Coordinates[0] + dirX);
            int yDistance = Math.Abs(this.Coordinates[1] - player.Coordinates[1] + dirY);
            return xDistance + yDistance;
        }
        //Actions
        public void Move(Player player)
        {
            char dir;
            int dirX = 0, dirY = 0;
            if (!WithinMoveRange(player)) return;
            int up = CalculateDistanceToPlayer(player, 0, -1);
            int right = CalculateDistanceToPlayer(player, 1, 0);
            int down = CalculateDistanceToPlayer(player, 0, 1);
            int left = CalculateDistanceToPlayer(player, -1, 0);
            int[] mins = { up, right, down, left };
            int cardinal = Array.IndexOf(mins, mins.Min());
            switch (cardinal)
            {
                case 0: //up
                    dirY = -1;
                    break;
                case 1: //right
                    dirX = 1;
                    break;
                case 2: //down
                    dirY = 1;
                    break;
                case 3: //left
                    dirX = -1;
                    break;
                default:
                    break;
            }
            LockEnemyMoveCoordinates(dirX, dirY);
        }
        public async void StartAsyncTaskMoveEnemy(Player player)
        {
            // Start the task.
            await Task.Run(() => Move(player));
        }
    }
}
