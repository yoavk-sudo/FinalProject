using FinalProject.Elements;
using FinalProject.Menus;

namespace FinalProject
{
    internal class Enemy
    {
        public static char Avatar = '¤';
        public static char TrollAvatar = 'ð';
        public static char DemonAvatar= 'Ý';
        public static int Diff = 1;
        readonly public string Type;
        readonly int _maxHP;
        public const int MAXRANGE = 5;
        public const int MELEERANGE = 1;
        int _currentHP;
        int _exp;
        readonly int _power;
        readonly int _gold;
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
            int hp = Map.LevelNumber + 1;
            int pow = Map.LevelNumber * Diff;
            if (type == "troll")
            {
                hp *= 2;
                pow *= 2;
                pow -= 2;
            }
            else if (type == "demon")
            {
                hp *= 4;
                pow *= 3;
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
        //Passive qualities
        public bool IsAlive()
        {
            if (this.HP <= 0)
            {
                EnemyList.RemoveFromList(this);
                if (this.Type == "demon") WinScreen.SpawnCrown();
                return false;
            }
            return true;
        }

        private int GoldAmount(int power)
        {
            int min = power * 2;
            int max = power * 3;
            int amount = Random.Shared.Next(min, max + 1);
            return amount * 9 * (1 / Diff);
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
