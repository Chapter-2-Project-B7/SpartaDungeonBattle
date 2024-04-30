namespace SpartaDungeonBattle
{
    public class Monster
    {
        public event EventHandler MonsterDied;
        public int Level { get; }
        public string Name { get; }
        public int AttackPower { get; }
        public int HealthPoint { get; set; }
        public bool IsDead { get; set; }

        
        public Monster(int level, string name, int attackPower, int healthPoint, bool isDead = false)
        {
            Level = level;
            Name = name;
            HealthPoint = healthPoint;
            AttackPower = attackPower;
            IsDead = isDead;
        }
        
        
        public void Die()
        {
            OnMonsterDied();
        }

        public virtual void OnMonsterDied()
        {
            MonsterDied?.Invoke(this, EventArgs.Empty);
        }

        public void TakeDamage(int damage)
        {
            if ((HealthPoint - damage) <= 0)
            {
                Console.Write("HP ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{HealthPoint}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" -> ");
                Console.ResetColor();
                Console.WriteLine("Dead");
                Die();
                HealthPoint = 0;
                IsDead = true;
            }
            else
            {
                Console.Write("HP ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{HealthPoint}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" -> ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{HealthPoint -= damage}");
                Console.ResetColor();
            }
        }

        virtual public void PrintMonsterList(bool withNumber = false, int listIdx = 0)
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
