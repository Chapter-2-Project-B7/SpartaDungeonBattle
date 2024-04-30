using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public enum QuestStatus
    {
        InProgress,
        Completed,
        Failed
    }
    abstract public class Quest
    {
        
        public string Name { get; set; }
        //퀘스트 설명
        public string Description { get; set; }
        //아이템 클래스 생성하면 보상을 아이템으로 변경하도록 만들 수 있다.
        public string Reward { get; set; }
        public QuestStatus Status { get; set; }

        public Quest(string name, string description, string reward)
        {
            Name = name;
            Description = description;
            Reward = reward;
            Status = QuestStatus.InProgress;
        }

        

        //퀘스트 요구사항은 자식들이 오버라이딩 할 수 있도록
        abstract public void Request();


        public void ClearQuest()
        {
            Console.Clear();

            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine(Name);
            Console.WriteLine();
            Console.WriteLine(Description);
            Console.WriteLine();
            Request();
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine(Reward);
            Console.WriteLine();
            Console.WriteLine("1. 보상 받기");
            Console.WriteLine("2. 돌아가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }
    }
}
