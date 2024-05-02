using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public class Inventory
    {
        public List<Item> inventory;
        public List<Item> potionInventory;

        public Inventory()
        {
            inventory = new List<Item>();
            potionInventory = new List<Item>();
        }
        public void InitItem()
        {
            inventory = new List<Item>
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

            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].PrintItemStatDescription();
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
        }
            public void AddItem()
        {
            inventory.Add(new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500));
        }
        public void SaveInventoryData()
        {
            //Console.WriteLine("파일세이브");
            string fileName = "Inventory.json";
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string Serialized = JsonConvert.SerializeObject(this, Formatting.Indented, settings);
            //Serialized += JsonConvert.SerializeObject(inventory, Formatting.Indented, settings);
            File.WriteAllText(fileName, Serialized);
        }

        public void LoadInventoryData()
        {
            //Console.WriteLine("파일로드");
            string fileName = "Inventory.json";
            string json = File.ReadAllText(fileName);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var loadedData = JsonConvert.DeserializeObject<Inventory>(json, settings);

            this.inventory.Clear();
            foreach(var item in inventory)
            {
                Console.WriteLine($"{item.Name}");
            }
            this.inventory = loadedData.inventory;
            this.potionInventory = loadedData.potionInventory;  

            //inventory = JsonConvert.DeserializeObject<List<Item>>(json, settings);
        }
    }
}
