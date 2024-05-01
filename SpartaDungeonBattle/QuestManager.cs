using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDungeonBattle
{
    internal class QuestManager
    {
        private static QuestManager instance;

        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance;
            }
        }

        public List<Quest> quests;

        public QuestManager()
        {
            quests = new List<Quest>();
        }


        public void InitQuest()
        {
            Quest monsterKillQuest = new MonsterKillQuest();
            Quest levelUpQuest = new LevelUpQuest();


            quests.Add(monsterKillQuest);
            quests.Add(levelUpQuest);
        }

        
        public void UpdateQuests()
        {
            foreach (Quest quest in quests)
            {
                quest.CheckComplete();
            }
        }
        public void EnterQuest()
        {
            UpdateQuests();
            while (true)
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
                Console.WriteLine("0. 돌아가기");

                int choice = ConsoleUtility.PromptMenuChoice(0, i - 1);

                switch (choice)
                {
                    case 0: GameManager.Instance.ShowMainMenu(); return;
                    default:
                        if (quests[choice - 1].Status == QuestStatus.Completed)
                            ClearQuest(choice - 1);
                        else if (quests[choice - 1].Status == QuestStatus.None)
                            RecieveQuest(choice - 1);
                        else if (quests[choice - 1].Status == QuestStatus.InProgress)
                            ShowQuestProgress(choice - 1);
                        break;
                }
            }

        }

        public void RecieveQuest(int idx)
        {
            Console.Clear();
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            Console.WriteLine(quests[idx].Name);
            Console.WriteLine();
            Console.WriteLine(quests[idx].Description);
            Console.WriteLine();
            quests[idx].Request();
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine(quests[idx].Reward.Name);
            Console.WriteLine();
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");

            int choice = ConsoleUtility.PromptMenuChoice(1, 2);

            switch(choice)
            {
                case 1:
                    quests[idx].InProgressQuest(); //진행중으로 바꾼다.
                    Console.WriteLine("퀘스트 추가!!"); 
                    break;
                case 2: 
                    EnterQuest(); 
                    break;
            }
        }

        //진행상황 나타내주는 메서드
        public void ShowQuestProgress(int idx)
        {
            Console.Clear();
            Console.WriteLine("Quest!! - 진행 상황");
            Console.WriteLine();
            Console.WriteLine(quests[idx].Name);
            Console.WriteLine();
            Console.WriteLine(quests[idx].Description);
            Console.WriteLine();
            quests[idx].Request();
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine(quests[idx].Reward.Name);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            //퀘스트 포기를 작성?

            int choice = ConsoleUtility.PromptMenuChoice(0,0);

            switch (choice)
            {
                case 0:
                    break;
            }
        }

        public void ClearQuest(int idx)
        {
            Console.Clear();

            Console.WriteLine("Quest - Clear !!");
            Console.WriteLine();
            Console.WriteLine(quests[idx].Name);
            Console.WriteLine();
            Console.WriteLine(quests[idx].Description);
            Console.WriteLine();
            quests[idx].Request();
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine(quests[idx].Reward.Name);
            Console.WriteLine();
            Console.WriteLine("1. 보상 받기");
            Console.WriteLine("2. 돌아가기");
        

            int choice = ConsoleUtility.PromptMenuChoice(1, 2);

            switch(choice)
            {
                case 1: Console.WriteLine("보상받기"); break;
                case 2: break;
            }
        }
    }
}
