namespace SpartaDungeonBattle
{
    public class MonsterKillQuest : Quest
    {
        public int SlimeKilledCount { get; set; }
        public int TargetCount { get; set; }
        public MonsterKillQuest() : base(
                                    "마을을 위협하는 슬라임 처치",
                                    "이봐! 마을 근처에 슬라임들이 너무 많아졌다고 생각하지 않나?\n" +
                                    "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                                    "모험가인 자네가 좀 처치해주게!",
                                    new Item("슬라임 투구", "슬라임의 점액으로 만들어진 말랑말랑한 투구이다.", ItemType.ARMOR, 0, 2, 0, 700))
        {
            TargetCount = 5;
        }

        override public void HandleMonsterDied(object? sender, EventArgs e)
        {
            var monster = sender as Monster;
            if (monster != null)
            {
                if (monster.Name == "Slime" && Status == QuestStatus.InProgress) //진행중이어야함
                    SlimeKilledCount++;
            }
        }
        override public void Request()
        {
            Console.WriteLine($"- 슬라임 5마리 처치 ({SlimeKilledCount}/{TargetCount})");
        }

        override public void CheckComplete()
        {
            if (SlimeKilledCount >= TargetCount)
            {
                this.CompleteQuest();
            }
        }


    }
}
