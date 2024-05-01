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

        public void TakeDamage(int damage)
        {
            if ((HealthPoint - damage) <= 0)
            {
                Console.WriteLine($"HP {HealthPoint} -> Dead");
                HealthPoint = 0;
                IsDead = true;
                Die();
            }
            else
            {
                Console.WriteLine($"HP {HealthPoint} -> {HealthPoint -= damage}");
            }
        }

       
    }
}
