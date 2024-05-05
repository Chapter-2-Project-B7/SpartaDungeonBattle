using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public class DataManager
    {
        private static DataManager instance;
        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataManager();
                }
                return instance;
            }
        }

        public void DataMenu()
        {
            Console.Clear();
            Console.WriteLine("1. 저장하기");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine();
            Console.WriteLine("0. 돌아가기");

            int input = ConsoleUtility.PromptMenuChoice(0, 2);

            switch(input)
            {
                case 0:
                    GameManager.Instance.ShowMainMenu();
                    break;
                case 1:
                    SaveData();
                    GameManager.Instance.ShowMainMenu();
                    break;
                case 2:
                    LoadData();
                    GameManager.Instance.ShowMainMenu();
                    break;
            }
        }
        public void SaveData()
        {
            QuestManager.Instance.SaveQuestData();
            GameManager.Instance.inventory.SaveInventoryData();
            GameManager.Instance.store.SaveStoreData();
            GameManager.Instance.player.SavePlayerData();
            GameManager.Instance.stage.SaveStageData();
            Console.WriteLine("데이터가 저장되었습니다!");
            Thread.Sleep(1000);
        }

        public void LoadData()
        {
            QuestManager.Instance.LoadQuestData();
            GameManager.Instance.inventory.LoadInventoryData();
            GameManager.Instance.store.LoadStoreData();
            GameManager.Instance.player = GameManager.Instance.player.LoadPlayerData();
            GameManager.Instance.SetStage(GameManager.Instance.player);
            GameManager.Instance.stage.LoadStageData();
            Console.WriteLine("데이터를 불러왔습니다!");
            Thread.Sleep(1000);
        }
        
    }
}
