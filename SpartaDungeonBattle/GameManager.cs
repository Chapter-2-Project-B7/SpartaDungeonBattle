using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SpartaDungeonBattle
{
    enum MainMenu
    {
        EndGame,
        Status,
        Battle,
        Inventory,
        Store,
        Quest,
        Data
    }

    enum StatusMenu
    {
        Exit
    }

    enum BattleMenu
    {
        Exit,
        Attack
    }

    enum SelectMonster
    {
        FirstMonster = 1,
        SecondMonster = 2,
        ThirdMonster = 3
    }

    enum BattlePhase
    {
        Next
    }

    [Serializable]
    public class GameManager
    {
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public Player player;

        public List<Monster> monsters;
        public List<Monster> randomMonsters;


        public List<Item> inventory;
        public List<Item> potionInventory;
        public List<Item> storeInventory;


        //클리어시 들어있는 아이템 리스트
        public List<Item> clearItemList;

        public Stage stage;

        public int stageCount = 0;

        public GameManager()
        {
            InitializeGame();
        }
        public void SaveGameManagerData()
        {
            //Console.WriteLine("파일세이브");
            string fileName = "GameManager.json";
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string Serialized = JsonConvert.SerializeObject(GameManager.instance, Formatting.Indented, settings);
            
            //Serialized += JsonConvert.SerializeObject(inventory, Formatting.Indented, settings);
            File.WriteAllText(fileName, Serialized);
        }

        public void LoadGameManagerData()
        {
            //Console.WriteLine("파일로드");
            string fileName = "GameManager.json";
            string json = File.ReadAllText(fileName);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            if (instance != null)
            {
                Console.WriteLine("이미 있습니다.");
                
            }
            else
            {
                
                instance = JsonConvert.DeserializeObject<GameManager>(json, settings);
            }

            //inventory = JsonConvert.DeserializeObject<List<Item>>(json, settings);
        }

        private void InitializeGame()
        {
            QuestManager.Instance.InitQuest(); //퀘스트 초기화

            string playerName = "";

            player = new Player(Player.JobType.Warrior, playerName);
            randomMonsters = new List<Monster>();
            clearItemList = new List<Item>();

            monsters = new List<Monster>
            {
                new Monster(1, "Slime", 5, 10),
                new Monster(4, "Golem", 8, 20),
                new Monster(6, "Ghost", 15, 15)
            };

            inventory = new List<Item>
            {
                new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500),
                new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000)
            };

            potionInventory = new List<Item>
            {
                new Item("체력 포션", "체력 회복", ItemType.POTION, 0, 0, 0, 100)
            };

            storeInventory = new List<Item>
            {
                new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500),
                new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000),
                new Item("골든 헬름", "희귀한 투구", ItemType.ARMOR, 0, 9, 0, 2000)
            };

            stage = new Stage(player, monsters, randomMonsters, clearItemList);
        }

        public void StartGame()
        {
            Console.Clear();
            ConsoleUtility.PrintGameHeader();
            ShowCreateCharacterMenu();
        }

        public void ShowCreateCharacterMenu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">> ");

            while (true)
            {
                player.Name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(player.Name))
                {
                    player.Name = player.Name;
                    ShowMainMenu();
                    break;
                }
                else
                {
                    Console.WriteLine("이름을 입력해주세요.");
                    Console.Write(">> ");
                }
            }
        }

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            ConsoleUtility.PrintTextHighlights("2. 전투 시작 (현재 진행 : ", $"{stageCount}", "층)");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 상점");
            Console.WriteLine("5. 퀘스트");
            Console.WriteLine("6. 저장/불러오기");
            Console.WriteLine("0. 게임 종료");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 6);

            switch ((MainMenu)choice)
            {
                case MainMenu.Status:
                    ShowStatusMenu();
                    break;

                case MainMenu.Battle:
                    stage.StageStart();
                    break;

                case MainMenu.Inventory:
                    ShowInventoryMenu();
                    break;

                case MainMenu.Store:
                    ShowStoreMenu();
                    break;
                case MainMenu.Quest:
                    QuestManager.Instance.EnterQuest();
                    break;
                case MainMenu.Data:
                    DataManager.Instance.DataMenu();
                    break;
                case MainMenu.EndGame:
                    break;
            }
        }

        private void ShowStatusMenu()
        {
            int bonusAtk = inventory.Select(item => item.IsEquipped ? item.Atk : 0).Sum();
            int bonusDef = inventory.Select(item => item.IsEquipped ? item.Def : 0).Sum();
            int bonusHp = inventory.Select(item => item.IsEquipped ? item.Hp : 0).Sum();

            Console.Clear();
            ConsoleUtility.ShowTitle("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            ConsoleUtility.PrintTextHighlights("공격력 : ", (player.AttackPower + bonusAtk).ToString(), bonusAtk > 0 ? $" (+{bonusAtk})" : "");
            ConsoleUtility.PrintTextHighlights("방어력 : ", (player.DefensePower + bonusDef).ToString(), bonusDef > 0 ? $" (+{bonusDef})" : "");
            ConsoleUtility.PrintTextHighlights("체  력 : ", (player.HealthPoint + bonusHp).ToString(), bonusHp > 0 ? $" (+{bonusHp})" : "");
            ConsoleUtility.PrintTextHighlights("Gold : ", $"{player.Gold}", " G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 0);

            switch ((StatusMenu)choice)
            {
                case StatusMenu.Exit:
                    ShowMainMenu();
                    break;
            }
        }

        private void ShowInventoryMenu()
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

            switch (ConsoleUtility.PromptMenuChoice(0, 2))
            {
                case 0:
                    ShowMainMenu();
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

            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int KeyInput = ConsoleUtility.PromptMenuChoice(0, inventory.Count);

            switch (KeyInput)
            {
                case 0:
                    ShowInventoryMenu();
                    break;

                default:
                    inventory[KeyInput - 1].ToggleEquipStatus();
                    if (inventory[KeyInput - 1].Type == ItemType.WEAPON)
                    {
                        if (inventory[KeyInput - 1].IsEquipped) player.EquipItem(ItemType.WEAPON, inventory[KeyInput - 1].Atk);
                        else player.TakeOffItem(ItemType.WEAPON, inventory[KeyInput - 1].Atk);
                    }
                    else
                    {
                        if (inventory[KeyInput - 1].IsEquipped) player.EquipItem(ItemType.ARMOR, inventory[KeyInput - 1].Def);
                        else player.TakeOffItem(ItemType.ARMOR, inventory[KeyInput - 1].Def);
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

        private void ShowStoreMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            ConsoleUtility.PrintTextHighlights("", player.Gold.ToString(), " G");
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
                    ShowMainMenu();
                    break;
                case 1:
                    PurchaseMenu();
                    break;
            }
        }

        private void PurchaseMenu(string? prompt = null)
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
            ConsoleUtility.PrintTextHighlights("", player.Gold.ToString(), " G");
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
                    else if (player.Gold >= storeInventory[keyInput - 1].Price)
                    {
                        player.Gold -= storeInventory[keyInput - 1].Price;
                        storeInventory[keyInput - 1].Purchase();
                        inventory.Add(storeInventory[keyInput - 1]);
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
    }
}
