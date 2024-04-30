namespace SpartaDungeonBattle
{
    internal class Monster
    {
        public event EventHandler MonsterDied;
        public int Level { get; }
        public string Name { get; }
        public int AttackPower { get; }
        public int HealthPoint { get; set; }
        public bool IsDead { get; set; }

        public Monster(
            int level,
            string name,
            int attackPower,
            int healthPoint,
            bool isDead = false
        )
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
                Console.Write($"HP {HealthPoint} -> Dead");
                HealthPoint = 0;
                IsDead = true;
            }
            else
            {
                Console.WriteLine($"HP {HealthPoint} -> {HealthPoint -= damage}");
            }
        }

       
    }
}
