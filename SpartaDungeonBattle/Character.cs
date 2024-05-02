namespace SpartaDungeonBattle
{
    public abstract class Character
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int AttackPower { get; set; }
        public virtual int HealthPoint { get; set; }
        public bool IsDead { get; set; }

        public Random Random = new Random();

        public virtual (int, bool) CalculateDamage()
        {
            int critical = Random.Next(1, 100);

            if (critical <= 15)
            {
                int criticalDamage = (int)Math.Ceiling(AttackPower * 1.6f);
                return (criticalDamage, true);
            }
            else
            {
                int min = AttackPower - (int)Math.Ceiling(AttackPower * 0.1f);
                int max = AttackPower + (int)Math.Ceiling(AttackPower * 0.1f);
                int randomDamage = Random.Next(min, max);
                return (randomDamage, false);
            }
        }

        public virtual void TakeDamage(int damage, bool isCritical)
        {
            int evasionRate = Random.Next(1, 100);

            if (evasionRate <= 10)
            {
                ConsoleUtility.PrintTextHighlights("Lv.", $"{Level}", $" {Name} 을(를) 공격했지만 아무 일도 일어나지 않았습니다.");
            }
            else
            {
                if (isCritical)
                {
                    Console.Write("Lv.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{Level}");
                    Console.ResetColor();
                    Console.Write($" {Name} 을(를) 맞췄습니다. [데미지 : ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{damage}");
                    Console.ResetColor();
                    Console.WriteLine("] - 치명타 공격!!");
                }
                else
                {
                    Console.Write("Lv.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{Level}");
                    Console.ResetColor();
                    Console.Write($" {Name} 을(를) 맞췄습니다. [데미지 : ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{damage}");
                    Console.ResetColor();
                    Console.WriteLine("]");
                }
                Console.WriteLine();
                ConsoleUtility.PrintTextHighlights("Lv.", $"{Level}", $" {Name}");

                if ((HealthPoint - damage) <= 0)
                {
                    ConsoleUtility.PrintTextHighlights("HP ", $"{HealthPoint}", " -> Dead");
                    HealthPoint = 0;
                    IsDead = true;
                }
                else
                {
                    Console.Write("HP ");
                    ConsoleUtility.PrintAllTextHighlights($"{HealthPoint}", " -> ", $"{HealthPoint -= damage}");
                }
            }
        }
    }
}
