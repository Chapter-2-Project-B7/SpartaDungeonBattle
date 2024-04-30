using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDungeonBattle
{
    public class QuestManager
    {
        public List<Quest> quests;

        public QuestManager()
        {
            quests = new List<Quest>();
        }


        public void InitQuest()
        {
            Quest monsterKillQuest = new MonsterKillQuest();


            quests.Add(monsterKillQuest);
        }

        public void EnterQuest()
        {
            Console.Clear();
            int i = 1;
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            foreach (Quest quest in quests)
            {
                Console.WriteLine($"{i}. {quest.Name}");
                i++;
            }
            Console.WriteLine();
            //Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.WriteLine("0. 돌아가기");

            int choice = ConsoleUtility.PromptMenuChoice(0,i-1);

            switch(choice)
            {
                case 0: Console.WriteLine("메인메뉴로 돌아가기"); break;
                default: RecieveQuest(choice); break;
            }

        }

        public void RecieveQuest(int idx)
        {
            Console.Clear();
            idx--;
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine(quests[idx].Name);
            Console.WriteLine();
            Console.WriteLine(quests[idx].Description);
            Console.WriteLine();
            //Request는 자식 클래스에서 작성후 부르면됨
            quests[idx].Request();
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine(quests[idx].Reward);
            Console.WriteLine();
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
           // Console.WriteLine("원하시는 행동을 입력해주세요.");

            int choice = ConsoleUtility.PromptMenuChoice(1, 3);

            switch(choice)
            {
                case 1: Console.WriteLine("퀘스트 추가!!"); break;
                case 2: EnterQuest(); break;
            }
        }

        /*public void ClearQuest()
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
        }*/
    }
}
