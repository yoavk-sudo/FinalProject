using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Weapon
    {
        string _weaponName;
        int _damage;
        
        public Weapon(string name, int damage)
        {
            weaponName = name;
            weaponDamage = damage;
        }
        public string weaponName
        { get { return _weaponName; } set { _weaponName = value; } }
        public int weaponDamage
        { get { return _damage; } set { _damage = value; } }
    }
}
