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

        public GameManager()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            player = new Player(1, "르탄이", "전사", 10, 5, 100, 10000);
        }

        public void StartGame()
        {
            Console.Clear();

            ShowMainMenu();
        }

        private void EndGame() { }

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

            int input = int.Parse(Console.ReadLine());

            switch ((MainMenu)input)
            {
                case MainMenu.Status:
                    ShowStatusMenu();
                    break;

                case MainMenu.Battle:
                    ShowBattleMenu();
                    break;

                case MainMenu.EndGame:
                    EndGame();
                    break;
            }
        }

        private void ShowStatusMenu()
        {
            Console.Clear();

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            Console.WriteLine($"공격력 : {player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체 력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold}");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = int.Parse(Console.ReadLine());

            switch ((StatusMenu)input)
            {
                case StatusMenu.Exit:
                    ShowMainMenu();
                    break;
            }
        }

        // 배틀 메뉴
        // (플레이어 또는 몬스터 전부가 죽을 때 까지 반복)
        // 플레이어 턴
        // 몬스터 턴
        // (플레이어 또는 몬스터 전부가 죽으면)
        // 배틀 결과

        private void ShowBattleMenu()
        {
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();
            // 몬스터 목록
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/100");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = int.Parse(Console.ReadLine());

            switch ((BattleMenu)input)
            {
                case BattleMenu.Exit:
                    ShowMainMenu();
                    break;

                case BattleMenu.Attack:
                    ShowPlayerTurn();
                    break;
            }
        }

        private void ShowPlayerTurn()
        {
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();
            // 몬스터 목록 (번호 포함)
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine("HP 100/100");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();

            int input = int.Parse(Console.ReadLine());

            switch ((PlayerTurn)input)
            {
                case PlayerTurn.Cancel:
                    ShowBattleMenu();
                    break;

                case PlayerTurn.FirstMonster:
                    ShowPlayerAttack();
                    break;

                case PlayerTurn.SecondMonster:
                    ShowPlayerAttack();
                    break;

                case PlayerTurn.ThirdMonster:
                    ShowPlayerAttack();
                    break;
            }
        }

        // 해당 몬스터를 매개변수로 받아와야하나?
        private void ShowPlayerAttack()
        {
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine("Chad 의 공격!");
            Console.WriteLine("Lv.3 공허충 을(를) 맞췄습니다. [데미지 : 10]");
            Console.WriteLine();
            Console.WriteLine("Lv.3 공허충");
            Console.WriteLine("HP 10 -> Dead");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int input = int.Parse(Console.ReadLine());

            switch ((BattleScene)input)
            {
                case BattleScene.Next:
                    ShowMonsterAttack();
                    break;
            }
        }

        // 몬스터 공격시 마다 반복
        private void ShowMonsterAttack()
        {
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();
            // 몬스터 공격 메세지
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine("HP 100 -> 94");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int input = int.Parse(Console.ReadLine());

            switch ((BattleScene)input)
            {
                case BattleScene.Next:
                    ShowPlayerTurn();
                    break;
            }
        }

        private void ShowBattleResult()
        {
            Console.Clear();

            Console.WriteLine("Battle!! - Result");
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

            int input = int.Parse(Console.ReadLine());

            switch ((BattleScene)input)
            {
                case BattleScene.Next:
                    ShowMainMenu();
                    break;
            }
        }
    }
}
