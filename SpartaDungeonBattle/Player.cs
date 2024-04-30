namespace SpartaDungeonBattle
{
    public class Player
    {
        enum CharacterClass : byte
        {
            Warrior
        }

        public int Level { get; }
        public string Name { get; }
        public string Job { get; }
        public int AttackPower { get; }
        public int DefensePower { get; }
        public int HealthPoint { get; set; }
        public int Gold { get; set; }
        public bool IsDead { get; set; }

        // csharpier-ignore
        public Player(int level, string name, string job, int attackPower, int defensePower, int healthPoint, int gold, bool isDead = false)
        {
            Level = level;
            Name = name;
            Job = job;
            AttackPower = attackPower;
            DefensePower = defensePower;
            HealthPoint = healthPoint;
            Gold = gold;
            IsDead = isDead;
        }

        public void TakeDamage(int damage)
        {
            if ((HealthPoint - damage) <= 0)
            {
                Console.Write($"HP {HealthPoint} -> Dead");
                HealthPoint = 0;
                IsDead = true;
            }
            else
            {
                Console.Write($"HP {HealthPoint} -> {HealthPoint -= damage}");
            }
        }
    }
}
