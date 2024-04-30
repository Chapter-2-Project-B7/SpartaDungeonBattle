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

        // csharpier-ignore
        public Player(JobType jobType)
        {
            ChangePlayerJob(jobType);
        }

        public void TakeDamage(int damage)
        {
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
