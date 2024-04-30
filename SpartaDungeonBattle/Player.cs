namespace SpartaDungeonBattle
{
    internal class Player
    {
        public enum JobType : byte
        {
            Warrior,
            Magician,
            Archer
        }

        public int Level { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int AttackPower { get; set; }
        public int DefensePower { get; set; }
        public int HealthPoint { get; set; }
        public int ManaPoint { get; set; }
        public int Gold { get; set; }
        public bool IsDead { get; set; }
        public JobType EnumJob { get; set; }
        public PlayerSkill[] Skills { get; set; }

        private Random random = new Random();

        public Player(JobType jobType)
        {
            ChangePlayerJob(jobType);
        }

        public (int, bool) CalculateDamage()
        {
            int critical = random.Next(1, 100);

            if (critical <= 15)
            {
                int criticalDamage = (int)Math.Ceiling(AttackPower * 1.6f);
                return (criticalDamage, true);
            }
            else
            {
                int min = AttackPower - (int)Math.Ceiling(AttackPower * 0.1f);
                int max = AttackPower + (int)Math.Ceiling(AttackPower * 0.1f);
                int randomDamage = random.Next(min, max);
                return (randomDamage, false);
            }
        }

        public void TakeDamage(int damage, bool isCritical)
        {
            int evasionRate = random.Next(1, 100);

            if (evasionRate <= 10)
            {
                Console.WriteLine($"Lv.{Level} {Name} 을(를) 공격했지만 아무 일도 일어나지 않았습니다.");
            }
            else
            {
                if (isCritical)
                {
                    Console.WriteLine($"Lv.{Level} {Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                }
                else
                {
                    Console.WriteLine($"Lv.{Level} {Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                }
                Console.WriteLine();
                Console.WriteLine($"Lv.{Level} {Name}");

                if ((HealthPoint - damage) <= 0)
                {
                    Console.WriteLine($"HP {HealthPoint} -> Dead");
                    HealthPoint = 0;
                    IsDead = true;
                }
                else
                {
                    Console.WriteLine($"HP {HealthPoint} -> {HealthPoint -= damage}");
                }
            }
        }

        public bool UseMana(int mana)
        {
            if (ManaPoint < mana)
            {
                Console.WriteLine("마나가 부족합니다!");

                return false;
            }
            else
            {
                Console.Write("MP ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{ManaPoint}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" -> ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{ManaPoint -= mana}");
                Console.ResetColor();

                return true;
            }
        }

        public void ChangePlayerJob(JobType playerJob)
        {
            switch (playerJob)
            {
                case JobType.Warrior:
                {
                    Level = 1;
                    Name = "Chad";
                    Job = "전사";
                    AttackPower = 10;
                    DefensePower = 5;
                    HealthPoint = 100;
                    ManaPoint = 20;
                    Gold = 1500;
                    IsDead = false;
                    EnumJob = JobType.Warrior;

                    Skills = new PlayerSkill[2];
                    Skills[0] = new WarriorSkill_AlphaStrike(AttackPower);
                    Skills[1] = new WarriorSkill_DoubleStrike(AttackPower);

                    break;
                }
                case JobType.Magician:
                {
                    Job = "마법사";
                    EnumJob = JobType.Magician;
                    break;
                }
                case JobType.Archer:
                {
                    Job = "궁수";
                    EnumJob = JobType.Archer;
                    break;
                }
            }
        }
    }
}
