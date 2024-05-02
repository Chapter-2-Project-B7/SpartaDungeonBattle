namespace SpartaDungeonBattle
{
    public class Monster : Character
    {
        public event EventHandler MonsterDied;

        //몬스터가 떨굴수있는 아이템리스트
        internal List<Item> DropItemList { get; set; }

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
            DropItemList = new List<Item>();
            InitDropItemList();
            //몬스터가 생성될때 퀘스트 라인 연결
            for (int i = 0; i < QuestManager.Instance.quests.Count; i++)
            {
                this.MonsterDied += QuestManager.Instance.quests[i].HandleMonsterDied;
            }
        }


        public void InitDropItemList()
        {
            DropItemList.Add(new Item("체력 포션", "체력 회복", ItemType.POTION, 0, 0, 0, 100));
            DropItemList.Add(new Item("마나 포션", "마나 회복", ItemType.POTION, 0, 0, 0, 100));
            DropItemList.Add(new Item("장화", "발 보호대", ItemType.ARMOR, 0, 5, 0, 300));
        }

        internal void DropItem()
        {
            Random random = new Random();
            int idx = random.Next(0,DropItemList.Count);
            if (DropItemList[idx] != null)
            {
                GameManager.Instance.clearItemList.Add(DropItemList[idx]);
            }
        }

        public void Die()
        {
            OnMonsterDied();
            DropItem();
        }

        public virtual void OnMonsterDied()
        {
            MonsterDied?.Invoke(this, EventArgs.Empty);
        }

        public override void TakeDamage(int damage, bool isCritical)
        {
            int evasionRate = Random.Next(1, 100);

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
                    Die();
                }
                else
                {
                    Console.Write("HP ");
                    ConsoleUtility.PrintTextSectionsHighlights($"{HealthPoint}", " -> ", $"{HealthPoint -= damage}");
                }
            }
        }
    }
}
