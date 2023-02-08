using FinalProject.Elements;

namespace FinalProject
{
    internal class Enemy
    {
        public static char Avatar = '¤';
        public static char TrollAvatar = 'ð';
        readonly public string Type;
        readonly int _maxHP;
        int _currentHP;
        readonly int _power;
        public static int Diff = 1;
        readonly int _gold;
        int _exp;
        public const int MAXRANGE = 5;
        public const int MELEERANGE = 1;
        //Constructor
        public Enemy(string type, int maxHP, int power)
        {
            Type = type;
            _maxHP = maxHP;
            _currentHP = _maxHP;
            _power = power;
            _gold = GoldAmount(power);
            _exp = Map.LevelNumber * 2;
            if (Type == "troll") _exp *= 2;
            EnemyList.AddToList(this);
        }
        public static Enemy CreateEnemy(string type) ///////////////////////////////////////////
        {
            int hp = Map.LevelNumber;
            int pow = Map.LevelNumber * Diff;
            if(type == "troll")
            {
                hp *= 2;
                pow *= 2;
                pow -= 2;
            }
            Enemy enemy = new Enemy(type, hp, pow);
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
        public int EXP { get { return _exp * Diff; } set { _exp = value; } }
        //Passive qualities
        private int GoldAmount(int power)
        {
            int min = power * 2;
            int max = power * 3;
            int amount = Random.Shared.Next(min, max + 1);
            return amount * 10 * (1 / Diff);
        }
        public bool IsAlive()
        {
            if (this.HP <= 0)
            {
                EnemyList.RemoveFromList(this);
                return false;
            }
            return true;
        }
        //Passive Actions
        public void TakeDamage(int damage, Player player)
        {
            HP -= damage;
            if (HP < 0)
            {
                HP = 0;
                player.GetGold(_gold);
                player.GainEXP(_exp);
                EnemyList.RemoveFromList(this);
            }
        }
        public static void EnemyAttack(Enemy enemy, Player player)
        {
            player.TakeDamage(enemy._power);
        }
    }
}
