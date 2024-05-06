using Newtonsoft.Json;

namespace SpartaDungeonBattle
{
    public class Player : Character
    {
        public enum JobType : byte
        {
            Warrior,
            Magician,
            Archer
        }

        //전사 기본 스탯
        public int WarriorAtk = 10;
        public int WarriorDef = 5;
        public int WarriorHp = 100;
        public int WarriorMp = 50;

        //마법사 기본 스탯
        public int MagicianAtk = 13;
        public int MagicianDef = 2;
        public int MagicianHp = 60;
        public int MagicianMp = 80;

        //궁수 기본 스탯
        public int ArcherAtk = 15;
        public int ArcherDef = 3;
        public int ArcherHp = 80;
        public int ArcherMp = 30;

        public string Job { get; set; }

        //공격력 관련
        public int TotalAtk { get; set; }           //총 공격력
        public int ItemAtk { get; set; }            //아이템으로 인한 추가 공격력

        //방어력 관련
        public int DefensePower { get; set; }       //기본 방어력
        public int TotalDef { get; set; }           //총 방어력
        public int ItemDef { get; set; }            //아이템으로 인한 추가 방어력

        //체력관련
        private int healthPoint;                    //현재 체력
        public override int HealthPoint             //현재 체력 ( public )
        {
            get { return healthPoint; }
            set 
            { 
                healthPoint = value;
                if(healthPoint > MaxHealthPoint) healthPoint = MaxHealthPoint;
            }
        }
        public int MaxHealthPoint { get; set; }     //최대 체력

        //마나관련
        private int manaPoint;                      //현재 마나
        public int ManaPoint                        //현재 마나 ( public )
        {
            get { return manaPoint; }
            set
            {
                manaPoint = value;
                if (manaPoint > MaxManaPoint) manaPoint = MaxManaPoint;
                if (manaPoint < 0) manaPoint = 0;
            }
        }
        public int MaxManaPoint { get; set; }       //최대 마나

        public int Gold { get; set; }
        public JobType EnumJob { get; set; }

        //스킬 관련
        public PlayerSkill Skill1 { get; set; }     //스킬1
        public PlayerSkill Skill2 { get; set; }     //스킬2
        public bool IsSkillCasting { get; set; }    //스킬 캐스팅 중인지 확인용
        public PlayerSkill ActiveSkill { get; set; }//사용할려는 스킬

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

        public override void TakeDamage(int damage, bool isCritical)
        {
            int evasionRate = Random.Next(1, 100);

            int defenseValue = (int)Math.Ceiling((float)TotalDef / damage);

            int totalDamage = (damage - defenseValue) <= 0 ? 1 : damage - defenseValue;

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
                    Console.Write($"{totalDamage}");
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
                    Console.Write($"{totalDamage}");
                    Console.ResetColor();
                    Console.WriteLine("]");
                }
                Console.WriteLine();
                ConsoleUtility.PrintTextHighlights("Lv.", $"{Level}", $" {Name}");

                if ((HealthPoint - totalDamage) <= 0)
                {
                    ConsoleUtility.PrintTextHighlights("HP ", $"{HealthPoint}", " -> Dead");
                    HealthPoint = 0;
                    IsDead = true;
                }
                else
                {
                    Console.Write("HP ");
                    ConsoleUtility.PrintAllTextHighlights($"{HealthPoint}", " -> ", $"{HealthPoint -= totalDamage}");
                }
            }
        }

        public bool UseMana(int skillNum)
        {
            switch (skillNum)
            {
                case 1:
                    {
                        if (Skill1.Mana <= ManaPoint) return true;
                        else return false;
                    }

                case 2:
                    {
                        if (Skill2.Mana <= ManaPoint) return true;
                        else return false;
                    }

                default:
                    return false;
            }
        }

        public void UseSkill()
        {
            Console.Write("MP ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{ManaPoint}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" -> ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{ManaPoint -= ActiveSkill.Mana}");
            Console.ResetColor();
        }

        public void ChangePlayerJob(JobType playerJob, string playerName)
        {
            switch (playerJob)
            {
                case JobType.Warrior:
                    {
                        Skill1 = new WarriorSkill_AlphaStrike(TotalAtk);
                        Skill2 = new WarriorSkill_DoubleStrike(TotalAtk);

                        Level = 1;
                        SetLevel(Level);
                        Name = playerName;
                        Job = "전사";
                        AttackPower = WarriorAtk;
                        DefensePower = WarriorDef;
                        //MaxHealthPoint = WarriorHp;
                        MaxHealthPoint = 1;
                        HealthPoint = MaxHealthPoint;
                        MaxManaPoint = WarriorMp;
                        ManaPoint = MaxManaPoint;
                        Gold = 1500;
                        IsDead = false;
                        EnumJob = JobType.Warrior;
                        CurrentExp = 0;
                        TotalAtk = AttackPower;
                        TotalDef = DefensePower;
                        ItemAtk = 0;
                        ItemDef = 0;

                    break;
                    }
                case JobType.Magician:
                    {
                        Skill1 = new MagicSkill_FireBall(TotalAtk);
                        Skill2 = new MagicSkill_Heal(TotalAtk);

                        Level = 1;
                        SetLevel(Level);
                        Name = playerName;
                        Job = "마법사";
                        AttackPower = MagicianAtk;
                        DefensePower = MagicianDef;
                        MaxHealthPoint = MagicianHp;
                        HealthPoint = MaxHealthPoint;
                        MaxManaPoint = MagicianMp;
                        ManaPoint = MaxManaPoint;
                        Gold = 1500;
                        IsDead = false;
                        EnumJob = JobType.Magician;
                        CurrentExp = 0;
                        TotalAtk = AttackPower;
                        TotalDef = DefensePower;
                        ItemAtk = 0;
                        ItemDef = 0;

                        break;
                    }
                case JobType.Archer:
                    {
                        Skill1 = new ArcherSkill_HeadShot(TotalAtk);
                        Skill2 = new ArcherSkill_Focus(TotalAtk);

                        Level = 1;
                        SetLevel(Level);
                        Name = playerName;
                        Job = "궁수";
                        AttackPower = ArcherAtk;
                        DefensePower = ArcherDef;
                        MaxHealthPoint = ArcherHp;
                        HealthPoint = MaxHealthPoint;
                        MaxManaPoint = ArcherMp;
                        ManaPoint = MaxManaPoint;
                        Gold = 1500;
                        IsDead = false;
                        EnumJob = JobType.Archer;
                        CurrentExp = 0;
                        TotalAtk = AttackPower;
                        TotalDef = DefensePower;
                        ItemAtk = 0;
                        ItemDef = 0;

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

            //레벨업 문구
            Console.Write(" -> ");
            ConsoleUtility.PrintTextHighlights("Lv.", $"{Level}", $" {Name}");
            Console.Write(" -> ");
            ConsoleUtility.PrintTextHighlights($"기본 공격력: {AttackPower} ", "+1");
            Console.Write(" -> ");
            ConsoleUtility.PrintTextHighlights($"기본 방어력: {DefensePower} ", "+1");

            SetLevel(Level);
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
            Skill1.SetSkill(TotalAtk);
        }

        public void EquipItem(ItemType equipment, int equipmentStat)
        {
            if (equipment == ItemType.POTION) return;

            if(equipment == ItemType.WEAPON)
            {
                ItemAtk += equipmentStat;
                TotalAtk = AttackPower + ItemAtk;
                Skill1.SetSkill(TotalAtk);
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
                Skill1.SetSkill(TotalAtk);
            }
            else if (equipment == ItemType.ARMOR)
            {
                ItemDef -= equipmentStat;
                TotalDef = DefensePower + ItemDef;
            }
        }

        public void SavePlayerData()
        {
            string fileName = "Player.json";
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string Serialized = JsonConvert.SerializeObject(this, Formatting.Indented, settings);
            File.WriteAllText(fileName, Serialized);

        }

        public Player LoadPlayerData()
        {
            string fileName = "Player.json";
            FileInfo fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists)
            {
                Console.WriteLine($"플레이어 저장 파일이 존재 하지 않습니다.");
                Thread.Sleep(1000);
                return null ;
            }
            string json = File.ReadAllText(fileName);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            Player? data = JsonConvert.DeserializeObject<Player>(json, settings);
            return data;
        }
    }
}
