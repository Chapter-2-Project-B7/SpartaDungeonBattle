namespace SpartaDungeonBattle
{
    enum MainMenu
    {
        EndGame,
        Status,
        Battle
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

    enum PlayerTurn
    {
        Cancel,
        FirstMonster,
        SecondMonster,
        ThirdMonster
    }

    enum BattleScene
    {
        Next
    }

    public class GameManager
    {
        private Player player;
        private List<Monster> monsters;

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

            switch ((PlayerTurn)choice)
            {
                case PlayerTurn.Cancel:
                    ShowBattleMenu();
                    break;

                case PlayerTurn.FirstMonster:
                    ShowPlayerPhase((int)PlayerTurn.FirstMonster);
                    break;

                case PlayerTurn.SecondMonster:
                    ShowPlayerPhase((int)PlayerTurn.SecondMonster);
                    break;

                case PlayerTurn.ThirdMonster:
                    ShowPlayerPhase((int)PlayerTurn.ThirdMonster);
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

            switch ((BattleScene)choice)
            {
                case BattleScene.Next:
                    ShowMonsterPhase();
                    break;
            }
        }

        // TODO: 죽은 몬스터 공격 X
        private void ShowMonsterPhase()
        {
            for (int i = 0; i < monsters.Count; i++)
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
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 0);

            switch ((BattleScene)choice)
            {
                case BattleScene.Next:
                    ShowSelectMonster();
                    break;
            }
        }

        // TODO: ShowBattleResult 조건 연결
        private void ShowBattleResult()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("Battle!! - Result");
            Console.WriteLine();
            // 배틀 결과 (승리 또는 패배)
            Console.WriteLine();
            Console.WriteLine("던전에서 몬스터 3마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine("HP 100 -> 74");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 0);

            switch ((BattleScene)choice)
            {
                case BattleScene.Next:
                    ShowMainMenu();
                    break;
            }
        }
    }
}
