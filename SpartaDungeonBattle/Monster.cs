namespace SpartaDungeonBattle
{
    public class Monster
    {
        public int Level { get; }
        public string Name { get; }
        public int AttackPower { get; }
        public int HealthPoint { get; set; }
        public bool IsDead { get; set; }

        // csharpier-ignore
        public Monster(int level, string name, int attackPower, int healthPoint, bool isDead = false)
        {
            Level = level;
            Name = name;
            HealthPoint = healthPoint;
            AttackPower = attackPower;
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
