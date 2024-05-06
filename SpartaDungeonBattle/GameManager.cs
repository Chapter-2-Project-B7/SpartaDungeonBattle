using Newtonsoft.Json;
using System.Numerics;
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
        Attack,
        Skill
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

        public Inventory inventory;
        public Store store;

        public List<Item> clearItemList;

        public Stage stage;

        public GameManager()
        {
            InitializeGame();
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
                new Monster(2, "Mimic", 5, 15),
                new Monster(3, "Goblin", 9, 10),
                new Monster(4, "Golem", 8, 20),
                new Monster(5, "Troll", 8, 25),
                new Monster(6, "Ghost", 15, 15)
            };
            //인벤토리 초기화
            inventory = new Inventory();
            inventory.InitItem();
            //상점 초기화
            store = new Store();
            store.InitStore();

            stage = new Stage(player, monsters, randomMonsters, clearItemList);
        }
        public void SetStage(Player p)
        {
            stage = new Stage(p, monsters, randomMonsters, clearItemList);
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
                    ShowSelectCharacterJobMenu();
                    break;
                }
                else
                {
                    Console.WriteLine("이름을 입력해주세요.");
                    Console.Write(">> ");
                }
            }
        }

        public void ShowSelectCharacterJobMenu()
        {
            Console.Clear();
            Console.WriteLine("직업을 선택할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("1. 전사");
            ConsoleUtility.PrintTextHighlights("공격력 : ", (player.WarriorAtk).ToString());
            ConsoleUtility.PrintTextHighlights("방어력 : ", (player.WarriorDef).ToString());
            ConsoleUtility.PrintTextHighlights("체  력 : ", (player.WarriorHp).ToString());
            ConsoleUtility.PrintTextHighlights("마  나 : ", (player.WarriorMp).ToString());
            Console.WriteLine();

            Console.WriteLine("2. 마법사");
            ConsoleUtility.PrintTextHighlights("공격력 : ", (player.MagicianAtk).ToString());
            ConsoleUtility.PrintTextHighlights("방어력 : ", (player.MagicianDef).ToString());
            ConsoleUtility.PrintTextHighlights("체  력 : ", (player.MagicianHp).ToString());
            ConsoleUtility.PrintTextHighlights("마  나 : ", (player.MagicianMp).ToString());
            Console.WriteLine();

            Console.WriteLine("3. 궁수");
            ConsoleUtility.PrintTextHighlights("공격력 : ", (player.ArcherAtk).ToString());
            ConsoleUtility.PrintTextHighlights("방어력 : ", (player.ArcherDef).ToString());
            ConsoleUtility.PrintTextHighlights("체  력 : ", (player.ArcherHp).ToString());
            ConsoleUtility.PrintTextHighlights("마  나 : ", (player.ArcherMp).ToString());
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(1, 3);

            switch (choice)
            {
                case 1:
                    player.EnumJob = Player.JobType.Warrior;
                    player.Job = "전사";
                    player.ChangePlayerJob(player.EnumJob, player.Name);
                    ShowMainMenu();
                    break;
                case 2:
                    player.EnumJob = Player.JobType.Magician;
                    player.Job = "마법사";
                    player.ChangePlayerJob(player.EnumJob, player.Name);
                    ShowMainMenu();
                    break;
                case 3:
                    player.EnumJob = Player.JobType.Archer;
                    player.Job = "궁수";
                    player.ChangePlayerJob(player.EnumJob, player.Name);
                    ShowMainMenu();
                    break;
            }

        }
        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            ConsoleUtility.PrintTextHighlights("2. 전투 시작 (현재 진행 : ", $"{stage.StageCount}", "층)");
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
                    inventory.ShowInventoryMenu();
                    break;
                case MainMenu.Store:
                    store.ShowStoreMenu();
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
            int bonusAtk = inventory.equipInventory.Select(item => item.IsEquipped ? item.Atk : 0).Sum();
            int bonusDef = inventory.equipInventory.Select(item => item.IsEquipped ? item.Def : 0).Sum();
            int bonusHp = inventory.equipInventory.Select(item => item.IsEquipped ? item.Hp : 0).Sum();

            Console.Clear();
            ConsoleUtility.ShowTitle("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            ConsoleUtility.PrintTextHighlights("공격력 : ", (player.AttackPower + bonusAtk).ToString(), bonusAtk > 0 ? $" (+{bonusAtk})" : "");
            ConsoleUtility.PrintTextHighlights("방어력 : ", (player.DefensePower + bonusDef).ToString(), bonusDef > 0 ? $" (+{bonusDef})" : "");
            ConsoleUtility.PrintTextHighlights("체  력 : ", (player.HealthPoint + bonusHp).ToString(), bonusHp > 0 ? $" (+{bonusHp})" : "");
            ConsoleUtility.PrintTextHighlights("마  나 : ", (player.ManaPoint).ToString());
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

       
    }
}
