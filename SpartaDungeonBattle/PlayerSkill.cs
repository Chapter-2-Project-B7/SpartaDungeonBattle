using System.Numerics;

namespace SpartaDungeonBattle
{
    public abstract class PlayerSkill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public int Target { get; set; }
        public int SkillNum { get; set; }
        public bool IsSkipTarget {  get; set; }

        public abstract void SetSkill(int attackDamage);
        public abstract int ActiveSkill();
        public virtual void PrintText(bool isEnoughMana)
        {
            if (!isEnoughMana)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{SkillNum}. {Name} - MP {Mana}");
                Console.WriteLine(Description);
                Console.ResetColor();
            }
            else
            {
                Console.Write(SkillNum);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($". {Name} ");
                Console.ResetColor();
                Console.Write("- MP ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{Mana}");
                Console.ResetColor();
                Console.WriteLine(Description);
            }
            Console.WriteLine();
        }
    }

    public class WarriorSkill_AlphaStrike : PlayerSkill
    {
        public WarriorSkill_AlphaStrike(int attackDamage)
        {
            Damage = attackDamage * 2;
            Name = "알파 스트라이크";
            Description = $"공격력({attackDamage}) * 2 로 하나의 적을 공격합니다.";
            Mana = 10;
            Target = 1;
            SkillNum = 1;
            IsSkipTarget = false;
        }

        public override void SetSkill(int attackDamage)
        {
            Damage = attackDamage * 2;
            Description = $"{Damage} 대미지로 하나의 적을 공격합니다.";
        }

        public override int ActiveSkill()
        {
            return Damage;
        }
    }

    public class WarriorSkill_DoubleStrike : PlayerSkill
    {
        public WarriorSkill_DoubleStrike(int attackDamage)
        {
            Damage = (int)Math.Round(attackDamage * 1.5, MidpointRounding.AwayFromZero);
            Name = "더블 스트라이크";
            Description = $"공격력({attackDamage}) * 1.5 로 랜덤하게 2명의 적을 랜덤으로 공격합니다";
            Mana = 15;
            Target = 2;
            SkillNum = 2;
            IsSkipTarget = true;
        }

        public override void SetSkill(int attackDamage)
        {
            Damage = (int)Math.Round(attackDamage * 1.5, MidpointRounding.AwayFromZero);
            Description = $"{Damage} 대미지로 랜덤하게 2명의 적을 랜덤으로 공격합니다";
        }

        public override int ActiveSkill()
        {
            return Damage;
        }
    }

    public class MagicSkill_FireBall : PlayerSkill
    {
        public MagicSkill_FireBall(int attackDamage)
        {
            Damage = (int)Math.Round(attackDamage * 1.8, MidpointRounding.AwayFromZero);
            Name = "메테오";
            Description = $"공격력({attackDamage}) * 1.8 로 적 모두를 공격합니다.";
            Mana = 50;
            Target = 9;
            SkillNum = 1;
            IsSkipTarget = true;
        }

        public override void SetSkill(int attackDamage)
        {
            Damage = (int)Math.Round(attackDamage * 1.8, MidpointRounding.AwayFromZero);
            Description = $"{Damage}의 피해를 적 모두에게 공격합니다.";
        }

        public override int ActiveSkill()
        {
            return Damage;
        }
    }

    public class MagicSkill_Heal : PlayerSkill
    {
        public MagicSkill_Heal(int attackDamage)
        {
            Damage = (int)Math.Round(attackDamage * 1.5, MidpointRounding.AwayFromZero);
            Name = "힐";
            Description = $"공격력({attackDamage}) * 1.5 만큼 자신의 체력을 회복합니다.";
            Mana = 10;
            Target = 0;
            SkillNum = 2;
            IsSkipTarget = true;
        }

        public override void SetSkill(int attackDamage)
        {
            Damage = (int)Math.Round(attackDamage * 1.5, MidpointRounding.AwayFromZero);
            Description = $"{Damage}만큼 자신의 체력을 회복합니다.";
        }

        public override int ActiveSkill()
        {
            var player = GameManager.Instance.player;
            int heal = player.MaxHealthPoint - player.HealthPoint < Damage ? player.MaxHealthPoint - player.HealthPoint : Damage;

            Console.WriteLine();
            Console.WriteLine($"{heal}만큼 피가 회복됐습니다.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{player.HealthPoint}");
            Console.ResetColor();
            Console.Write(" -> ");
            player.HealthPoint += Damage * 2;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{player.HealthPoint}");
            Console.ResetColor();
            Console.WriteLine();

            return Damage;
        }
    }

    public class ArcherSkill_HeadShot : PlayerSkill
    {
        public ArcherSkill_HeadShot(int attackDamage)
        {
            Damage = attackDamage;
            Name = "헤드샷";
            Description = $"공격력({attackDamage})의 1 ~ 3배의 대미지로 하나의 적을 공격합니다.";
            Mana = 15;
            Target = 1;
            SkillNum = 1;
            IsSkipTarget = false;
        }

        public override void SetSkill(int attackDamage)
        {
            Description = $"공격력({attackDamage})의 1 ~ 3배의 대미지로 하나의 적을 공격합니다.";
            Damage = attackDamage;
        }

        public override int ActiveSkill()
        {
            Random rand = new Random();
            float randomDamage = rand.Next(1, 301) * 0.01f;
            int finalDamage = (int)Math.Round(Damage * randomDamage, MidpointRounding.AwayFromZero);
            return finalDamage;
        }
    }

    public class ArcherSkill_Focus : PlayerSkill
    {
        public ArcherSkill_Focus(int attackDamage)
        {
            Damage = attackDamage;
            Name = "집중";
            Description = $"{Damage}만큼 적 1명에게 피해를 주고, 그만큼 자신의 기본 공격력을 영구적으로 올립니다.";
            Mana = 10;
            Target = 1;
            SkillNum = 2;
            IsSkipTarget = false;
        }

        public override void SetSkill(int attackDamage)
        {
            Damage = attackDamage;
            Description = $"{Damage}만큼 적 1명에게 피해를 주고, 그만큼 자신의 기본 공격력을 영구적으로 올립니다.";
        }

        public override int ActiveSkill()
        {
            int playerAtk = GameManager.Instance.player.AttackPower;
            int addAtk = (int)Math.Round(Damage * 0.1, MidpointRounding.AwayFromZero);

            Console.WriteLine();
            Console.WriteLine($"{addAtk}만큼 기본 공격력이 상승됐습니다.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{playerAtk}");
            Console.ResetColor();
            Console.Write(" -> ");
            playerAtk += addAtk;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{playerAtk}");
            Console.ResetColor();
            Console.WriteLine();

            return Damage;
        }
    }
}
