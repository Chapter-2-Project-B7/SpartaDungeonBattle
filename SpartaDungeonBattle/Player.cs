using System.Numerics;
using System.Reflection.Emit;

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
        private int healthPoint;
        public int HealthPoint
        {
            get { return healthPoint; }
            set
            {
                healthPoint = (healthPoint + value) >= 100 ? 100 : healthPoint + value;
            }
        }
        public int ManaPoint { get; set; }
        public int Gold { get; set; }
        public bool IsDead { get; set; }
        public JobType EnumJob { get; set; }
        public PlayerSkill[] Skills { get; set; }
        public int MaxExp { get; set; }
        public int CurrentExp { get; set; }

        private Random random = new Random();

        public Player(JobType jobType, string playerName)
        {
            ChangePlayerJob(jobType, playerName);
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
                    ConsoleUtility.PrintTextSectionsHighlights($"{HealthPoint}", " -> ", $"{HealthPoint -= damage}");
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

        public void ChangePlayerJob(JobType playerJob, string playerName)
        {
            switch (playerJob)
            {
                case JobType.Warrior:
                {
                    Level = 1;
                    SetLevel(Level);
                    Name = playerName;
                    Job = "전사";
                    AttackPower = 10;
                    DefensePower = 5;
                    HealthPoint = 100;
                    ManaPoint = 20;
                    Gold = 1500;
                    IsDead = false;
                    EnumJob = JobType.Warrior;
                    CurrentExp = 0;

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

        public void GetExp(int exp)
        {
            CurrentExp += exp;
            //반복문인 이유는 얻는 경험치통이 2번 레벨업 할 수 있는 양이 들어올 걸 대비해서
            while (true)
            {
                if (CurrentExp >= MaxExp)
                {
                    int remainExp;
                    remainExp = CurrentExp - MaxExp;
                    LevelUp();
                    CurrentExp = remainExp;
                }
                else
                    break;
            }
        }

        public void LevelUp()
        {
            Level++;
            SetLevel(Level);

            //레벨업 문구
            Console.Write(" -> ");
            ConsoleUtility.PrintTextHighlights("Lv.", $"{Level}", $" {Name}");
        }

        public void SetLevel(int level)
        {
            switch (level)
            {
                case 1:
                    MaxExp = 10;
                    break;
                case 2:
                    MaxExp = 35;
                    break;
                case 3:
                    MaxExp = 65;
                    break;
                case 4:
                    MaxExp = 100;
                    break;
                default:
                    MaxExp = 9999;
                    break;
            }
        }
    }
}
