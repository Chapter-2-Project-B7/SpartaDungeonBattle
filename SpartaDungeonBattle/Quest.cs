using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public enum QuestStatus
    {
        None,
        InProgress,
        Completed,
        Failed
    }
    abstract internal class Quest
    {
        public string Name { get; set; }
        //퀘스트 설명
        public string Description { get; set; }
        //아이템 클래스 생성하면 보상을 아이템으로 변경하도록 만들 수 있다.
        public Item Reward { get; set; }
        public bool IsRewarded { get; set; }//보상을 주었는가 확인

        public QuestStatus Status { get; set; }

        public Quest(string name, string description, Item reward)
        {
            Name = name;
            Description = description;
            Reward = reward;
            Status = QuestStatus.None;
            IsRewarded = false;
        }

        public void InProgressQuest()
        {
            Status = QuestStatus.InProgress;
        }
        public void CompleteQuest()
        {
            Status = QuestStatus.Completed;
        }
        public void FailedQuest()
        {
            Status = QuestStatus.Failed;
        }

        public void GetReward()
        {
            if (!IsRewarded)
            {
                GameManager.Instance.inventory.Add(Reward);
                Console.WriteLine($"{Reward.Name}을 보상으로 얻었습니다!");
                IsRewarded = true;
            }
            else
            {
                Console.WriteLine("이미 보상을 받았습니다.");
            }
            Thread.Sleep(1000);
        }
        //퀘스트 요구사항은 자식들이 오버라이딩 할 수 있도록
        abstract public void Request();
        abstract public void CheckComplete();
        virtual public void HandleMonsterDied(object? sender, EventArgs e)
        {

        }
    }
}
