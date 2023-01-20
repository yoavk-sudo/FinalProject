using FinalProject.Magic;

namespace FinalProject
{
    internal class Player
    {
        int _maxHP = 20;
        int _maxMP = 10;
        int _manaRegen = 0;
        int _manaCounter = 5;
        private int _counter = 0;

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
                Console.SetCursorPosition(0, 20);
            }
        }
        private void TrySetCursorInRange()
        {
            Console.SetCursorPosition(Coordinates[0], Coordinates[1] - 1);
        }

        Weapon weapon = new Weapon("nothing", 0);
        Armor armor = new Armor("nothing", 0);
        
        //constructor
        public Player(string name)
        {
            this.PlayerName = name;
            this.HP = _maxHP;
            this.MP = _maxMP;
            Console.CursorVisible = false;
        }
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
        public int Experience
        {
            get; set;
        }
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
        public void TakeDamage(int damage)
        {
            float rand = new Random().Next(1, 11);
            if (rand <= this.Evasion)
            {
                Log.PrintMessage("You evaded an attack!", ConsoleColor.White);
                return;
            }
            int total = damage - armor.ArmorDef;
            if (total >= 0)//if shield>damage do nothing
            {
                HP = HP - total;
                if (HP <= 0) HP = 0;
                Log.PrintMessage($"You took {total} damage!" , ConsoleColor.Red);
                HUD.DisplayHUD(this);
                return;
            }
            Log.PrintMessage($"You blocked that attack!", ConsoleColor.Blue);
        }
        public void Heal(int healAmount)
        {
            if(HP == _maxHP) return;
            int logHeal = 0;
            if(_maxHP >= HP + healAmount) logHeal = healAmount;
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
            if(this.HP <= 0) return false;
            return true;
        }
        private void MPRegen()
        {
            if (_manaRegen++ < _manaCounter) return;
            _manaRegen = 0;
            if (this.MP >= this._maxMP) return;
            this.MP++;
        }
        #endregion
        #region Player actions

        // Describes certain features of target, 3 steps ahead at most.
        // Need add direction + Inspect info
        // add Inspect levels?
        public void Inspect() 
        {
            for (int i = 1; i < 4; i++)
            {
                if (this.Coordinates[1] + i == 1)
                {
                    Console.WriteLine("Something here! Appraise:");
                    break;
                }    
            }
        }
        public async Task CastSpell(char input)
        {
            int spell;
            switch(input)
            {
                case 'v':
                    spell = Spells.FindSpell(typeof(FireBall));
                    break;
                case 'm':
                    spell = Spells.FindSpell(typeof(Lightning));
                    break;
                default: 
                    return;
            }
            if (spell == -1) return;
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
            int[] XY = DirectionToXY();
            try
            {
                if (Map.MapCol[Coordinates[0] + XY[0], Coordinates[1] + XY[1]] == 1) return; //if obstructed
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.WriteLine("2");
            }
            Spells.ClearSpellSlate();
            //int x = Coordinates[0] + XY[0];
            //int y = Coordinates[1] + XY[1];
            int[] magicCoor = GetCoordinates(XY, spell);
            ////int[] magicCoor = GetCoordinates();
            ////magicCoor[0] += XY[0];
            ////magicCoor[1] += XY[1];
            ////Console.SetCursorPosition(magicCoor[0], magicCoor[1]);
            ////Console.ForegroundColor = Spells.spells[spell].Col;
            ////Console.Write(Spells.spells[spell].Symbol);
            ////Console.ResetColor();
            this.MP -= Spells.spells[spell].MPCost;
            HUD.DisplayHUD(this);
            Console.Beep(150, 100);
            Spells.spells[spell].Charge = 0;
            Spells.DisplaySpells();
            Task<dynamic> t = await Task.Run(() => Spells.MoveSpell(Spells.spells[spell], magicCoor, Direction));
            Console.WriteLine("DFG");
        }
        private async Task MoveCollision(char input)
        {
            switch (input)
            {
                case 'w':
                    input = '▲';
                    Direction = "up";
                    break;
                case 'd':
                    input = '►';
                    Direction = "right";
                    break;
                case 's':
                    input = '▼';
                    Direction = "down";
                    break;
                case 'a':
                    input = '◄';
                    Direction = "left";
                    break;
                default:
                    break;
            }
            try
            {
                TrySetCursorInRange();
                //Console.SetCursorPosition(Coordinates[0], Coordinates[1] - 1);
            }
            catch(System.ArgumentOutOfRangeException)   // (System.IndexOutOfRangeException) 
            {
                Console.WriteLine("1");
            }
            //raise all spell charges///////////////////////////////////////////////////////////////////////////////////////
            int[] XY = DirectionToXY();
            try
            {
                if(Map.MapCol[Coordinates[0] + XY[0], Coordinates[1] + XY[1]] == 1) //if obstructed
                {
                    //Console.SetCursorPosition(Coordinates[0], Coordinates[1]); //position at current coordinates
                    //Console.ForegroundColor = ConsoleColor.Blue;
                    //Console.Write(input); //print player at current coordinates, new direction
                    //Console.ResetColor();
                    DrawPlayer(input);
                    return;
                }
            }
            catch(System.IndexOutOfRangeException)
            {
                Console.WriteLine("2");
            }
            //if not obstructed
            LockMethods.SetCursorLockAndOneSpace(Coordinates, 0, 0);//erase current player marker
            UpdateCoordinates(XY[0], XY[1]); //move up or down 1 tile
            DrawPlayer(input);
            //Console.SetCursorPosition(Coordinates[0], Coordinates[1]); //position at new coordinates
            //Console.ForegroundColor = ConsoleColor.Blue;
            //Console.Write(input); //print player at new coordinates
            //Console.ResetColor();
            //Console.SetCursorPosition(0, 20);
            Console.Beep(105, 100);
            Spells.RechargeSpells(); //Recharge spells
            Spells.DisplaySpells();
            HUD.DisplayHUD(this);
        }
        public async Task Move(char direction)
        {
            MoveCollision(direction);
            MPRegen();
            //HUD.UpdateHUD(this, HUD.Stats.MP);
        }
        public void BasicAttack()
        {

        }
        
        //Upon trying to interact, inspect coordinates in front of player
        //for this, 
        public void Interact()
        {
            int[] XY = DirectionToXY();
            if(Map.MapCol[Coordinates[0] + XY[0], Coordinates[1] + XY[1]] == 1)
            {
                //check list of objects in seperate class,
                //go through each object and check if its coordinates corrospond
                //with the one that was checked
                //if such an object was found, call an appropriate method
            }
        }
        #endregion
    }
}