using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public class Store
    {
        public List<Item> storeInventory;
        public Store()
        {
            storeInventory = new List<Item>();
        }

        public void InitStore()
        {
            storeInventory.Clear();
            storeInventory = new List<Item>
            {
                new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500),
                new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000),
                new Item("골든 헬름", "희귀한 투구", ItemType.ARMOR, 0, 9, 0, 2000)
            };

        }
        public void ShowStoreMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            ConsoleUtility.PrintTextHighlights("", GameManager.Instance.player.Gold.ToString(), " G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < storeInventory.Count; i++)
            {
                storeInventory[i].PrintStoreItemDescription();
            }

            Console.WriteLine("");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 0:
                    GameManager.Instance.ShowMainMenu();
                    break;
                case 1:
                    PurchaseMenu();
                    break;
            }
        }

        public void PurchaseMenu(string? prompt = null)
        {
            if (prompt != null)
            {
                // 1초간 메시지를 띄운 다음에 다시 진행
                Console.Clear();
                ConsoleUtility.ShowTitle(prompt);
                Thread.Sleep(1000);
            }

            Console.Clear();
            ConsoleUtility.ShowTitle("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            ConsoleUtility.PrintTextHighlights("", GameManager.Instance.player.Gold.ToString(), " G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < storeInventory.Count; i++)
            {
                storeInventory[i].PrintStoreItemDescription(true, i + 1);
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int keyInput = ConsoleUtility.PromptMenuChoice(0, storeInventory.Count);

            switch (keyInput)
            {
                case 0:
                    ShowStoreMenu();
                    break;

                default:
                    // 1 : 이미 구매한 경우
                    if (storeInventory[keyInput - 1].IsPurchased)
                    {
                        PurchaseMenu("이미 구매한 아이템입니다.");
                    }
                    // 2 : 돈이 충분해서 살 수 있는 경우
                    else if (GameManager.Instance.player.Gold >= storeInventory[keyInput - 1].Price)
                    {
                        GameManager.Instance.player.Gold -= storeInventory[keyInput - 1].Price;
                        storeInventory[keyInput - 1].Purchase();
                        GameManager.Instance.inventory.equipInventory.Add(storeInventory[keyInput - 1]);
                        PurchaseMenu();
                    }
                    // 3 : 돈이 모자라는 경우
                    else
                    {
                        PurchaseMenu("Gold가 부족합니다.");
                    }
                    break;
            }
        }

        public void SaveStoreData()
        {
            string fileName = "Store.json";
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string Serialized = JsonConvert.SerializeObject(this, Formatting.Indented, settings);
            File.WriteAllText(fileName, Serialized);
        }

        public void LoadStoreData()
        {
            string fileName = "Store.json";
            FileInfo fileInfo = new FileInfo(fileName);
            if(!fileInfo.Exists)
            {
                Console.WriteLine($"상점 저장 파일이 존재 하지 않습니다.");
                Thread.Sleep(1000);
                return;
            }
            string json = File.ReadAllText(fileName);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var loadedData = JsonConvert.DeserializeObject<Store>(json, settings);

            this.storeInventory.Clear();
            this.storeInventory = loadedData.storeInventory;   
        }




    }
}
