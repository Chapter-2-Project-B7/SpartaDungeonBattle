using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public class Inventory
    {
        public List<Item> equipInventory;
        public List<Item> potionInventory;

        public Inventory()
        {
            equipInventory = new List<Item>();
            potionInventory = new List<Item>();
        }
        public void InitItem()
        {
            equipInventory = new List<Item>
            {
                new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500),
                new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000)
            };
            potionInventory = new List<Item>
            {
                new Item("체력 포션", "체력 회복", ItemType.POTION, 0, 0, 0, 100)
            };
        }
        public void ShowInventoryMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록 - 장비]");

            for (int i = 0; i < equipInventory.Count; i++)
            {
                equipInventory[i].PrintItemStatDescription();
            }

            Console.WriteLine();
            Console.WriteLine("[아이템 목록 - 포션]");

            for (int i = 0; i < potionInventory.Count; i++)
            {
                potionInventory[i].PrintItemStatDescription();
            }

            Console.WriteLine("");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("2. 포션관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 2))
            {
                case 0:
                    GameManager.Instance.ShowMainMenu();
                    break;

                case 1:
                    EquipMenu();
                    break;

                case 2:
                    PotionMenu();
                    break;
            }
        }

        private void EquipMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < equipInventory.Count; i++)
            {
                equipInventory[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int KeyInput = ConsoleUtility.PromptMenuChoice(0, equipInventory.Count);

            switch (KeyInput)
            {
                case 0:
                    ShowInventoryMenu();
                    break;

                default:
                    equipInventory[KeyInput - 1].ToggleEquipStatus();
                    if (equipInventory[KeyInput - 1].Type == ItemType.WEAPON)
                    {
                        if (equipInventory[KeyInput - 1].IsEquipped) GameManager.Instance.player.EquipItem(ItemType.WEAPON, equipInventory[KeyInput - 1].Atk);
                        else GameManager.Instance.player.TakeOffItem(ItemType.WEAPON, equipInventory[KeyInput - 1].Atk);
                    }
                    else
                    {
                        if (equipInventory[KeyInput - 1].IsEquipped) GameManager.Instance.player.EquipItem(ItemType.ARMOR, equipInventory[KeyInput - 1].Def);
                        else GameManager.Instance.player.TakeOffItem(ItemType.ARMOR, equipInventory[KeyInput - 1].Def);
                    }
                    EquipMenu();
                    break;
            }
        }

        private void PotionMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("인벤토리 - 포션 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < potionInventory.Count; i++)
            {
                potionInventory[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int KeyInput = ConsoleUtility.PromptMenuChoice(0, potionInventory.Count);

            switch (KeyInput)
            {
                case 0:
                    ShowInventoryMenu();
                    break;

                default:
                    potionInventory[KeyInput - 1].UsePotion();
                    potionInventory.RemoveAt(KeyInput - 1);
                    PotionMenu();
                    break;
            }
        }


        public void SaveInventoryData()
        {
            string fileName = "Inventory.json";
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string Serialized = JsonConvert.SerializeObject(this, Formatting.Indented, settings);
            File.WriteAllText(fileName, Serialized);
        }

        public void LoadInventoryData()
        {
            string fileName = "Inventory.json";
            FileInfo fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists)
            {
                Console.WriteLine($"인벤토리 저장 파일이 존재 하지 않습니다.");
                Thread.Sleep(1000);
                return;
            }
            string json = File.ReadAllText(fileName);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var loadedData = JsonConvert.DeserializeObject<Inventory>(json, settings);

            this.equipInventory.Clear();
            this.equipInventory = loadedData.equipInventory;
            this.potionInventory.Clear();
            this.potionInventory = loadedData.potionInventory;  
        }
    }
}
