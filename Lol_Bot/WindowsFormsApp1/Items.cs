using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Items
    {
        bool IsActive = false;
        float coolDown = 0.0f;
        bool IsEnableWhileDead = false;

        string name;
        int cost;

        List<Items> components;

        int health;
        int __Mana; // Find English Word For it (Or not!!!)
        int __AD;
        int __AP;
        int armor;
        int magicResistance;
        int __CoolDownP;
        int attackSpeed;
        int critic;
        int movementSpeed;

        void Use()
        {
            //In this Situations We can not use this item 
            // So return immediately
            if (IsActive == false)
            {
                return;
            }

            // If not, Use it
            // Blah Blah Blah


            // Set CoolDown
        }
    }
}
