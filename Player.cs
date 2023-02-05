using FinalProject.Elements;
using FinalProject.Keys;
using FinalProject.Magic;
using FinalProject.Menus;

namespace FinalProject
{
    internal class Player
    {
        public static char avatar = '▲';
        int _maxHP = 20;
        int _maxMP = 5;
        int _manaRegen = 0;
        int _manaCounter = 5;
        public int ExpLvlCap = 10;
        const int TRAPDAMAGE = 1;
        const int MAXGOLD = 9999;
        Weapon weapon = new Weapon("nothing", 1);
        Armor armor = new Armor("nothing", 0);
        public Player(string name)
        {
            this.PlayerName = name;
            NewPlayerStatsReset();
            Console.CursorVisible = false;
        }

        private void NewPlayerStatsReset()
        {
            _maxHP = 20;
            _maxMP = 5;
            Level = 1;
            ExpLvlCap = 10;
            Experience = 9.5;
            this.HP = _maxHP;
            this.MP = _maxMP;
        }

        #region LockAsyncSetCursor
        private void LockWriteTrapVisible()
        {
            lock (LockMethods.ActionLock)
            {
                Console.SetCursorPosition(Coordinates[0], Coordinates[1]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('#');
                Console.ResetColor();
            }
        }
        private void UpdateCoordinates(int x, int y)
        {
            lock (LockMethods.ActionLock)
            {
                Coordinates[0] += x;
                Coordinates[1] += y;
            }
        }
        private int[] GetCoordinates(int[] XY, dynamic spell)
        {
            lock (LockMethods.ActionLock)
            {
                int[] magicCoor = new int[2];
                magicCoor[0] = Coordinates[0];
                magicCoor[1] = Coordinates[1];
                magicCoor[0] += XY[0];
                magicCoor[1] += XY[1];
                MP -= Spells.spells[spell].MPCost;
                HUD.DisplayHUD(this);
                if (spell == Spells.FindSpell(typeof(Heal))) return magicCoor;
                Console.SetCursorPosition(magicCoor[0], magicCoor[1]);
                Console.ForegroundColor = Spells.spells[spell].Col;
                Console.Write(Spells.spells[spell].Symbol);
                Console.ResetColor();
                return magicCoor;
            }
        }
        private void DrawPlayer(char avatar)
        {
            lock (LockMethods.ActionLock)
            {
                Console.SetCursorPosition(Coordinates[0], Coordinates[1]); //position at new coordinates
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(avatar); //print player at new coordinates
                Console.ResetColor();
            }
        }
        private void TrySetCursorInRange()
        {
            Console.SetCursorPosition(Coordinates[0], Coordinates[1] - 1);
        }
        #endregion

        //constructor
        #region Player Stats
        //attributes //will need to become private
        public string PlayerName
        {
            get; set;
        }
        public int HP
        {
            get; set;
        }
        public int MP
        {
            get; set;
        }
        public int Gold
        { 
            get; set; 
        }
        public float Evasion
        {
            get; set;
        } = 2f;
        public int Damage
        {
            get { return weapon.weaponDamage; }
        }
        public double Experience
        {
            get; set;
        } = 9.5;
        public int Level
        {
            get; set;
        } = 1;
        public int[] Coordinates //coordinates[x,y]
        {
            get; set;
        } = { 0, 0 };
        public string Direction
        {
            get; set;
        } = "up";
        #endregion

        public int[] DirectionToXY()
        {
            int xAxis = 0;
            int yAxis = 0;
            switch (Direction)
            {
                case "up":
                    xAxis = 0;
                    yAxis = -1;
                    break;
                case "right":
                    xAxis = 1;
                    yAxis = 0;
                    break;
                case "down":
                    xAxis = 0;
                    yAxis = 1;
                    break;
                case "left":
                    xAxis = -1;
                    yAxis = 0;
                    break;
            }
            int[] XY = { xAxis, yAxis };
            return XY;
        }

        #region Player passive "actions"
        public void TakeDamage(int damage, bool _isTrap)
        {
            float rand = new Random().Next(1, 11);
            if (rand <= this.Evasion && !_isTrap)
            {
                Log.PrintMessage("You evaded an attack!", ConsoleColor.Cyan);
                return;
            }
            int total = damage - armor.ArmorDef;
            if (total > 0)//if shield>damage do nothing
            {
                if (_isTrap) total = damage;
                HP = HP - total;
                if (HP <= 0) HP = 0;
                Log.PrintMessage($"You took {total} damage! HP is {HP}" , ConsoleColor.Red);
                HUD.DisplayHUD(this);
                if (HP == 0) Loss();
                return;
            }
            Log.PrintMessage($"You blocked that attack!", ConsoleColor.Blue);
        }

        private void Loss()
        {
            if (IsAlive()) return;
            Thread.Sleep(500);
            GameOver.GameOverDisplay(this);
        }

        public void Heal(int healAmount)
        {
            if(HP == _maxHP) return;
            int logHeal = 0;
            if(_maxHP >= HP + healAmount) logHeal = healAmount;
            else logHeal = _maxHP - HP;
            //else logHeal = _maxHP - HP;
            HP += healAmount;
            if (HP > _maxHP)
            {
                HP = _maxHP;
            }
            Log.PrintMessage($"You healed {logHeal} HP", ConsoleColor.Green);
            HUD.DisplayHUD(this);
        }
        public bool IsAlive()
        {
            if (HP > 0) return true;
            Map.IsAlive = false;
            return false;
        }
        private void MPRegen()
        {
            if (_manaRegen++ < _manaCounter) return;
            _manaRegen = 0;
            if (this.MP >= this._maxMP) return;
            this.MP++;
        }
        public void GetGold(int goldAmount)
        {
            Gold += goldAmount;
            Log.PrintMessage($"Got {goldAmount} gold!", ConsoleColor.Yellow);
            if (Gold > MAXGOLD) Gold = MAXGOLD;
            HUD.DisplayHUD(this);
        }
        public void GainEXP(int expAmount)
        {
            Experience+= expAmount;
            HUD.DisplayHUD(this);
            if (Experience < ExpLvlCap)
            {
                Log.PrintMessage($"Got {expAmount} EXP!", ConsoleColor.Green);
                return;
            }
            LevelUp();
        }
        public void LevelUp()
        {
            Log.PrintMessage("You have leveled up! Your level is: " + ++Level, ConsoleColor.Green);
            _maxHP += 10;
            _maxMP += 5;
            HP = _maxHP;
            MP= _maxMP;
            ExpLvlCap = Level * 10;
            Experience = 0;
            HUD.DisplayHUD(this);
            if (Level == 2)
            {
                Spells.spells[0].IsAcquired = true;
                Log.PrintMessage($"Learned Fireball! Press {Controls.KeyLayout["fireball"]} to launch a fireball", ConsoleColor.DarkGreen);
                Spells.DisplaySpells();
            }
            if (Level == 4)
            {
                Spells.spells[2].IsAcquired = true;
                Log.PrintMessage($"Learned Heal! Press {Controls.KeyLayout["heal"]} to heal", ConsoleColor.DarkGreen);
                Spells.DisplaySpells();
            }
            if (Level == 6)
            {
                Spells.spells[1].IsAcquired = true;
                Log.PrintMessage($"Learned Lightning! Press {Controls.KeyLayout["Lightning"]} to bring down a lightning bolt", ConsoleColor.DarkGreen);
                Spells.DisplaySpells();
            }
        }
        #endregion
        #region Player actions
        public async Task CastSpell(char input)
        {
            int spell;
            bool isHeal = false;
            switch(input)
            {
                case 'v':
                    spell = Spells.FindSpell(typeof(FireBall));
                    break;
                case 'u':
                    spell = Spells.FindSpell(typeof(Heal));
                    isHeal = true;
                    break;
                case 'm':
                    spell = Spells.FindSpell(typeof(Lightning));
                    break;
                default:
                    return;
            }
            if (spell == -1) return;
            if (!Spells.spells[spell].IsAcquired) return;
            if (Spells.spells[spell].Charge != 2)
            {
                //beep error
                return;
            }
            if(this.MP < Spells.spells[spell].MPCost)
            {
                Console.Beep(300, 300);
                Log.PrintMessage("Not enough MP!", ConsoleColor.Red);
                return;
            }
            if (isHeal) this.Heal(Magic.Heal.Power);
            int[] XY = DirectionToXY();
            if (Map.WhatIsInNextTile(Coordinates, XY) == 1 && !isHeal) return; //if obstructed
            Spells.ClearSpellSlate();
            Console.Beep(150, 100);
            Spells.spells[spell].Charge = 0;
            int[] magicCoor = GetCoordinates(XY, spell);
            Spells.DisplaySpells();
            if (isHeal) return;
            Task<int> t = await Task.Run(() => Spells.MoveSpell(Spells.spells[spell], magicCoor, Direction, this));
        }
        private async Task MoveCollision(char input)
        {
            switch (input)
            {
                case 'w':
                    input = avatar;// '▲';
                    Direction = "up";
                    break;
                case 'd':
                    input = avatar; //'►';
                    if(input == '▲') input = '►';
                    Direction = "right";
                    break;
                case 's':
                    input = avatar; //'▼';
                    if(input == '▲') input = '▼';
                    Direction = "down";
                    break;
                case 'a':
                    input = avatar; //'◄';
                    if(input == '▲') input = '◄';
                    Direction = "left";
                    break;
                default:
                    break;
            }
            try
            {
                TrySetCursorInRange();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("1");
            }
            int[] XY = DirectionToXY();
            if (Map.WhatIsInNextTile(Coordinates, XY) == 2)
            {
                Map.LoadNextLevel(this);
                return;
            }
            if (Map.WhatIsInNextTile(Coordinates, XY) == 1) //if obstructed
            {
                if (ElementsList.ElementByCoordinates(Coordinates) == '#') Console.BackgroundColor = ConsoleColor.Red;
                DrawPlayer(input);
                return;
            }
            //if not obstructed
            LockMethods.SetCursorLockAndOneSpace(Coordinates);//erase current player marker
            if (ElementsList.ElementByCoordinates(Coordinates) == '#') LockWriteTrapVisible();
            UpdateCoordinates(XY[0], XY[1]); //move up or down 1 tile
            if (ElementsList.ElementByCoordinates(Coordinates) == '#')
            {
                TakeDamage(TRAPDAMAGE, true);
                ElementsList.InteractWithElement(Coordinates, this);
                Console.BackgroundColor = ConsoleColor.Red;
            }
            DrawPlayer(input);
            Spells.RechargeSpells(); //Recharge spells
            Spells.DisplaySpells();
            HUD.DisplayHUD(this);
        }
        public async Task Move(char direction)
        {
            await MoveCollision(direction);
            MPRegen();
        }
        public void Interact()
        {
            int[] XY = DirectionToXY();
            int[] inFront = {Coordinates[0] + XY[0], Coordinates[1] + XY[1]};
            if (ElementsList.ElementByCoordinates(inFront) == '#') return; //Don't interact with traps
            if (EnemyList.EnemyByCoordinates(inFront) != null) DealDamage(inFront, Damage); //basic attack
            ElementsList.InteractWithElement(inFront, this);
        }
        public void DealDamage(int[] cor, int dmg)
        {
            if(EnemyList.EnemyByCoordinates(cor) == null) return;
            int critChance = Random.Shared.Next(1, 11);
            int hitChance = Random.Shared.Next(1, 11);
            if (hitChance < 2)
            {
                Log.PrintMessage("Enemy dodged your attack!", ConsoleColor.Red);
                return;
            }
            if (critChance < 2)
            {
                dmg *= 2;
                Log.PrintMessage($"You dealt {dmg} damage!", ConsoleColor.Green);
                Log.PrintMessage("CRIT", ConsoleColor.DarkYellow);
                EnemyList.EnemyByCoordinates(cor).TakeDamage(dmg, this);
                return;
            }
            Log.PrintMessage($"You dealt {dmg} damage!", ConsoleColor.Green);
            EnemyList.EnemyByCoordinates(cor).TakeDamage(dmg, this);
        }
        public void UsePotion()
        {
            if(!Inventory.IsInInventory('ß')) return;
            int healAmount = _maxHP / 2;
            Heal(healAmount);
            Inventory.RemoveFromInventory('ß');
        }
        #endregion
    }
}