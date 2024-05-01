using System.Threading;

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

        private Random random = new Random();

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
            //몬스터가 생성될때 퀘스트 라인 연결
            for (int i = 0; i < QuestManager.Instance.quests.Count; i++)
            {
                this.MonsterDied += QuestManager.Instance.quests[i].HandleMonsterDied;
            }
        }

        public void Die()
        {
            OnMonsterDied();
        }

        public virtual void OnMonsterDied()
        {
            MonsterDied?.Invoke(this, EventArgs.Empty);
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


    }
}
