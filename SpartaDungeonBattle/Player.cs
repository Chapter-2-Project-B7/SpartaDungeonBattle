namespace SpartaDungeonBattle
{
    internal class Player : Character
    {
        public enum JobType : byte
        {
            Warrior,
            Magician,
            Archer
        }

        public string Job { get; set; }

        //공격력 관련
        public int TotalAtk { get; set; }           //총 공격력
        public int ItemAtk { get; set; }            //아이템으로 인한 추가 공격력

        //방어력 관련
        public int DefensePower { get; set; }       //기본 방어력
        public int TotalDef { get; set; }           //총 방어력
        public int ItemDef { get; set; }            //아이템으로 인한 추가 방어력

        //체력관련
        private int healthPoint;
        public override int HealthPoint
        {
            get { return healthPoint; }
            set 
            { 
                healthPoint = value;
                if(healthPoint > 100) healthPoint = 100;
            }
        }

        public int ManaPoint { get; set; }
        public int Gold { get; set; }
        public JobType EnumJob { get; set; }
        public PlayerSkill[] Skills { get; set; }

        //경험치 관련
        public int MaxExp { get; set; }
        public int CurrentExp { get; set; }

        public Player(JobType jobType, string playerName)
        {
            ChangePlayerJob(jobType, playerName);
        }

        public override (int, bool) CalculateDamage()
        {
            int critical = Random.Next(1, 100);

            if (critical <= 15)
            {
                int criticalDamage = (int)Math.Ceiling(TotalAtk * 1.6f);
                return (criticalDamage, true);
            }
            else
            {
                int min = TotalAtk - (int)Math.Ceiling(TotalAtk * 0.1f);
                int max = TotalAtk + (int)Math.Ceiling(TotalAtk * 0.1f);
                int randomDamage = Random.Next(min, max);
                return (randomDamage, false);
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
                        TotalAtk = AttackPower;
                        TotalDef = DefensePower;
                        ItemAtk = 0;
                        ItemDef = 0;

                        Skills = new PlayerSkill[2];
                        Skills[0] = new WarriorSkill_AlphaStrike(TotalAtk);
                        Skills[1] = new WarriorSkill_DoubleStrike(TotalAtk);

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
            Console.Write(" -> ");
            ConsoleUtility.PrintTextHighlights("기본 공격력: ", " +1 ", $" {AttackPower}");
            Console.Write(" -> ");
            ConsoleUtility.PrintTextHighlights("기본 방어력: ", " +1 ", $" {DefensePower}");
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
            AttackPower = 10 + (level - 1);
            DefensePower = 5 + (level - 1);
            TotalAtk = AttackPower + ItemAtk;
            TotalDef = DefensePower + ItemDef;
        }

        public void EquipItem(ItemType equipment, int equipmentStat)
        {
            if (equipment == ItemType.POTION) return;

            if(equipment == ItemType.WEAPON)
            {
                ItemAtk += equipmentStat;
                TotalAtk = AttackPower + ItemAtk;
            }
            else if(equipment == ItemType.ARMOR)
            {
                ItemDef += equipmentStat;
                TotalDef = DefensePower + ItemDef;
            }
        }
        public void TakeOffItem(ItemType equipment, int equipmentStat)
        {
            if (equipment == ItemType.POTION) return;

            if (equipment == ItemType.WEAPON)
            {
                ItemAtk -= equipmentStat;
                TotalAtk = AttackPower + ItemAtk;
            }
            else if (equipment == ItemType.ARMOR)
            {
                ItemDef -= equipmentStat;
                TotalDef = DefensePower + ItemDef;
            }
        }

    }
}
