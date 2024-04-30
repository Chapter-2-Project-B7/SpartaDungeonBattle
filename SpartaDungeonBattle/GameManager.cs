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
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            Console.WriteLine($"공격력 : {player.AttackPower}");
            Console.WriteLine($"방어력 : {player.DefensePower}");
            Console.WriteLine($"채 력 : {player.HealthPoint}");
            Console.WriteLine($"Gold : {player.Gold}");
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
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].PrintMonsterList();
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.HealthPoint} / 100");
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
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].PrintMonsterList(true, i + 1);
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            // csharpier-ignore
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.HealthPoint} / 100");
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
            Random rand = new Random();
            int min = player.AttackPower - (int)Math.Ceiling(player.AttackPower * 0.1);
            int max = player.AttackPower + (int)Math.Ceiling(player.AttackPower * 0.1);
            int randomDamage = rand.Next(min, max);

            Monster monster = monsters[monsterNum - 1];

            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{player.Name} 의 공격!");
            // csharpier-ignore
            Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {randomDamage}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster.Level} {monster.Name}");
            monster.TakeDamage(randomDamage);
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
                Monster monster = monsters[i];

                if (monster.IsDead)
                {
                    continue;
                }
                else
                {
                    Random rand = new Random();
                    int min = monster.AttackPower - (int)Math.Ceiling(monster.AttackPower * 0.1);
                    int max = monster.AttackPower + (int)Math.Ceiling(monster.AttackPower * 0.1);
                    int randomDamage = rand.Next(min, max);

                    Console.Clear();
                    Console.WriteLine("Battle!!");
                    Console.WriteLine();
                    Console.Write($"Lv.{monster.Level} {monster.Name} 의 공격!");
                    Console.WriteLine($"{player.Name} 을(를) 맞췄습니다.  [데미지 : {randomDamage}]");
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{player.Level} {player.Name}");
                    player.TakeDamage(randomDamage);
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
            Console.WriteLine("Battle!! - Result");
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
            Console.WriteLine("Battle!! - Result");
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
    }
}
