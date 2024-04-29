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
                Console.WriteLine($"{HealthPoint} -> Dead");
                HealthPoint = 0;
                IsDead = true;
            }
            else
            {
                Console.WriteLine($"{HealthPoint} -> {HealthPoint -= damage}");
            }
        }

        public void PrintMonsterList(bool withNumber = false, int listIdx = 0)
        {
            if (IsDead == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (withNumber)
                {
                    Console.Write($"{listIdx} ");
                }
                Console.WriteLine($"Lv.{Level} {Name} Dead");
                Console.ResetColor();
            }
            else
            {
                if (withNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{listIdx} ");
                    Console.ResetColor();
                }

                Console.Write("Lv.");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{Level} ");
                Console.ResetColor();

                Console.Write($"{Name} HP ");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{HealthPoint}");
                Console.ResetColor();
            }
        }
    }
}
