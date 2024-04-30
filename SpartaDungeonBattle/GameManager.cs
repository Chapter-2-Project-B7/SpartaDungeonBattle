namespace SpartaDungeonBattle
{
    enum MainMenu
    {
        EndGame,
        Status,
        Battle,
        Inventory,
        Store
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
        Cancel,
        FirstMonster,
        SecondMonster,
        ThirdMonster
    }

    enum BattlePhase
    {
        Next
    }


    public class GameManager
    {
        private Player player;
        private List<Monster> monsters;

        private List<Item> inventory;
        private List<Item> storeInventory;

        public GameManager()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            player = new Player(1, "Chad", "전사", 10, 5, 100, 1500);
            monsters = [new Slime(), new Golem(), new Ghost()];
        }

        public void StartGame()
        {
            Console.Clear();
            ConsoleUtility.PrintGameHeader();
            ShowMainMenu();
        }

        private void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 상점");
            Console.WriteLine("0. 게임 종료");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 2);

            switch ((MainMenu)choice)
            {
                case MainMenu.Status:
                    ShowStatusMenu();
                    break;

                case MainMenu.Battle:
                    ShowBattleMenu();
                    break;

                case MainMenu.Inventory:
                    ShowInventoryMenu();
                    break;

                case MainMenu.Store:
                    ShowStoreMenu();
                    break;

                case MainMenu.EndGame:
                    break;
            }
        }

        private void ShowStatusMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            ConsoleUtility.PrintTextHighlights("공격력 : ", $"{player.AttackPower}");
            ConsoleUtility.PrintTextHighlights("방어력 : ", $"{player.DefensePower}");
            ConsoleUtility.PrintTextHighlights("채 력 : ", $"{player.HealthPoint}");
            ConsoleUtility.PrintTextHighlights("Gold : ", $"{player.Gold}");
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

        private void ShowBattleMenu()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].PrintMonsterList();
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            // csharpier-ignore
            ConsoleUtility.PrintTextHighlights("Lv.", $"{player.Level}", $"  {player.Name} ({player.Job})");
            Console.Write($"HP ");
            ConsoleUtility.PrintTextSectionsHighlights($"{player.HealthPoint}", "/", "100");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 1);

            switch ((BattleMenu)choice)
            {
                case BattleMenu.Exit:
                    ShowMainMenu();
                    break;

                case BattleMenu.Attack:
                    ShowSelectMonster();
                    break;
            }
        }

        // TODO: 죽은 몬스터 선택 X
        private void ShowSelectMonster()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].PrintMonsterList(true, i + 1);
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            // csharpier-ignore
            ConsoleUtility.PrintTextHighlights("Lv.", $"{player.Level}", $"  {player.Name} ({player.Job})");
            Console.Write($"HP ");
            ConsoleUtility.PrintTextSectionsHighlights($"{player.HealthPoint}", "/", "100");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, monsters.Count);

            switch ((SelectMonster)choice)
            {
                case SelectMonster.Cancel:
                    ShowBattleMenu();
                    break;

                case SelectMonster.FirstMonster:

                    ShowPlayerPhase((int)SelectMonster.FirstMonster);
                    break;

                case SelectMonster.SecondMonster:
                    ShowPlayerPhase((int)SelectMonster.SecondMonster);
                    break;

                case SelectMonster.ThirdMonster:
                    ShowPlayerPhase((int)SelectMonster.ThirdMonster);
                    break;
            }
        }

        private void ShowPlayerPhase(int monsterNum)
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("Battle!!");
            Console.WriteLine();
            Console.Write($"{player.Name} 의 공격");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("!");
            Console.ResetColor();
            Console.Write("Lv.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{monsters[monsterNum - 1].Level}");
            Console.ResetColor();
            Console.Write($" {monsters[monsterNum - 1].Name} 을(를) 맞췄습니다. [데미지 : ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{player.AttackPower}");
            Console.ResetColor();
            Console.WriteLine("]");
            Console.WriteLine();
            // csharpier-ignore
            ConsoleUtility.PrintTextHighlights("Lv.", $"{monsters[monsterNum - 1].Level}", $" {monsters[monsterNum - 1].Name}");
            monsters[monsterNum - 1].TakeDamage(player.AttackPower);
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 0);

            switch ((BattlePhase)choice)
            {
                case BattlePhase.Next:
                    if (CheckAllMonstersAreDead(monsters))
                    {
                        ShowVictoryResult();
                        break;
                    }
                    else
                    {
                        ShowMonsterPhase();
                    }
                    break;
            }
        }

        private void ShowMonsterPhase()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDead)
                {
                    continue;
                }
                else
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("Battle!!");
                    Console.WriteLine();
                    Console.Write("Lv.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{monsters[i].Level}");
                    Console.ResetColor();
                    Console.Write($" {monsters[i].Name}의 공격");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("!");
                    Console.ResetColor();
                    // csharpier-ignore
                    ConsoleUtility.PrintTextHighlights($"{player.Name} 을(를) 맞췄습니다.  [데미지 : ", $"{monsters[i].AttackPower}", $"]");
                    Console.WriteLine();
                    ConsoleUtility.PrintTextHighlights("Lv.", $"{player.Level}", $" {player.Name}");
                    player.TakeDamage(monsters[i].AttackPower);
                    Thread.Sleep(1000);
                    if (player.IsDead)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 0);

            switch ((BattlePhase)choice)
            {
                case BattlePhase.Next:
                    if (player.IsDead)
                    {
                        ShowLoseResult();
                        break;
                    }
                    else
                    {
                        ShowSelectMonster();
                        break;
                    }
            }
        }

        private void ShowVictoryResult()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("Battle!! - Result");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Victory");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP 100 -> {player.HealthPoint}");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 0);

            switch ((BattlePhase)choice)
            {
                case BattlePhase.Next:
                    ShowMainMenu();
                    break;
            }
        }

        private void ShowLoseResult()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("Battle!! - Result");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("You Lose");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine("HP 100 -> 0");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 0);

            switch ((BattlePhase)choice)
            {
                case BattlePhase.Next:
                    ShowMainMenu();
                    break;
            }
        }

        private bool CheckAllMonstersAreDead(List<Monster> monsters)
        {
            if (monsters.All(monster => monster.IsDead))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void ShowInventoryMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 인벤토리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].PrintItemStatDescription();
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");

            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 0:
                    ShowMainMenu();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }
        }

        private void EquipMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 인벤토리 - 장착 관리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].PrintItemStatDescription(true, i + 1);
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int KeyInput = ConsoleUtility.PromptMenuChoice(0, inventory.Count);

            switch (KeyInput)
            {
                case 0:
                    ShowInventoryMenu();
                    break;
                default:
                    inventory[KeyInput - 1].ToggleEquipStatus();
                    EquipMenu();
                    break;
            }
        }

        private void ShowStoreMenu()
        {
            Console.Clear();

            ConsoleUtility.ShowTitle("■ 상점 ■");
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

            ConsoleUtility.ShowTitle("■ 상점 ■");
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
