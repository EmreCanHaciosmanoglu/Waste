namespace WindowsFormsApp1
{
    class Character
    {
        string playerName;
        string league;

        public string Name;
        string Costume;

        public int __AD;
        int __AP;
        int armor;
        int magicResistance;
        int __CoolDownP;
        int attackSpeed;
        int critic;
        int movementSpeed;

        int magicPenetrationPer;
        int magicPenetrationVal;
        int armorPenetrationPer;
        int armorPenetrationVal;


        int qLevel;
        int qCoolDown;

        int wLevel;
        int wCoolDown;

        int eLevel;
        int eCoolDown;

        int rLevel;
        int rCoolDown;


        Passives passice;

        Skills q;
        Skills w;
        Skills e;
        Skills r;
        public Character(string name)
        {
            Name = name;
        }
    }
}
