namespace SpartaDungeonBattle
{
    internal abstract class PlayerSkill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }

        public abstract void SetSkill(int attackDamage);
    }

    internal class WarriorSkill_AlphaStrike : PlayerSkill
    {
        public WarriorSkill_AlphaStrike(int attackDamage)
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
    }

    internal class WarriorSkill_DoubleStrike : PlayerSkill
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
    }
}
