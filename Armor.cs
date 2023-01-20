using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Armor
    {
        string _armorName;
        int _armorDef;
        public Armor(string name, int def)
        {
            _armorName = name;
            _armorDef = def;
        }
        public string ArmorName
        { get { return _armorName; } set { _armorName = value; } }
        public int ArmorDef
        { get { return _armorDef; } set { _armorDef = value; } }
    }
}
