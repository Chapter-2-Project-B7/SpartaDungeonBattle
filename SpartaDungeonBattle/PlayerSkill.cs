namespace SpartaDungeonBattle
{
    public abstract class PlayerSkill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public int Target { get; set; }

        public abstract void SetSkill(int attackDamage);
        public virtual void PrintText(bool isEnoughMana)
        {
            if (!isEnoughMana) Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"1. {Name} - MP {Mana}");
            Console.WriteLine(Description);
            Console.ResetColor();
        }
    }

    public class WarriorSkill_AlphaStrike : PlayerSkill
    {
        public WarriorSkill_AlphaStrike(int attackDamage)
        {
            Name = "알파 스트라이크";
            Description = $"공격력({attackDamage}) * 2 로 하나의 적을 공격합니다.";
            Mana = 10;
            Damage = attackDamage * 2;
            Target = 1;
        }

        public override void SetSkill(int attackDamage)
        {
            Description = $"공격력({attackDamage}) * 2 로 하나의 적을 공격합니다.";
            Damage = attackDamage * 2;
        }

        //public override void PrintText(bool isEnoughMana)
        //{
        //    if(!isEnoughMana) Console.ForegroundColor = ConsoleColor.DarkGray;
        //    Console.WriteLine($"1. {Name} - MP {Mana}");
        //    Console.WriteLine(Description);
        //    Console.ResetColor();
        //}
    }

    public class WarriorSkill_DoubleStrike : PlayerSkill
    {
        public WarriorSkill_DoubleStrike(int attackDamage)
        {
            Name = "알파 스트라이크";
            Description = $"공격력({attackDamage}) * 2 로 하나의 적을 공격합니다.";
            Mana = 10;
            Damage = attackDamage * 2;
        }

        public override void SetSkill(int attackDamage)
        {
            Description = $"공격력({attackDamage}) * 2 로 하나의 적을 공격합니다.";
            Damage = attackDamage * 2;
        }

        //public override void PrintText(bool isEnoughMana)
        //{
        //    if (!isEnoughMana) Console.ForegroundColor = ConsoleColor.DarkGray;
        //    Console.WriteLine($"1. {Name} - MP {Mana}");
        //    Console.WriteLine(Description);
        //    Console.ResetColor();
        //}
    }
}
