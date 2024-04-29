using System.Runtime.CompilerServices;
using System.Threading;

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

        // GameManager 생성자
        public GameManager()
        {
            InitializeGame();
        }

        // 게임에 필요한 객체 초기화
        private void InitializeGame()
        {
            player = new Player(1, "르탄이", "전사", 10, 5, 100, 10000);
            monsters = [new Slime(), new Golem(), new Ghost()];
        }

        public void StartGame()
        {
            Console.Clear();

            ShowMainMenu();
        }

        private void EndGame() { }

        // 메인 메뉴
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

        // 상태 창
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
        private void ShowBattleMenu()
        {
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.WriteLine($"Lv.{monsters[i].Lv} {monsters[i].Name} HP {monsters[i].Hp}");
            }
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

        // 플레이어 턴 (몬스터 선택)
        private void ShowPlayerTurn()
        {
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1} Lv.{monsters[i].Lv} {monsters[i].Name} HP {monsters[i].Hp}"
                );
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/100");
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
                    ShowPlayerAttack((int)PlayerTurn.FirstMonster);
                    break;

                case PlayerTurn.SecondMonster:
                    ShowPlayerAttack((int)PlayerTurn.SecondMonster);
                    break;

                case PlayerTurn.ThirdMonster:
                    ShowPlayerAttack((int)PlayerTurn.ThirdMonster);
                    break;
            }
        }

        // 플레이어 턴
        private void ShowPlayerAttack(int monsterNum)
        {
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{player.Name}의 공격!");
            Console.WriteLine(
                $"Lv.{monsters[monsterNum - 1].Lv} {monsters[monsterNum - 1].Name} 을(를) 맞췄습니다. [데미지 : {player.Atk}]"
            );
            // 작업 중인 곳
            monsters[monsterNum - 1].TakeDamage(player.Atk);
            Console.WriteLine($"Lv.{monsters[monsterNum - 1].Lv} {monsters[monsterNum - 1].Name}");
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

        // 몬스터 턴
        // 몬스터 공격시 마다 반복
        private void ShowMonsterAttack()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                Console.WriteLine($"Lv.{monsters[i].Lv} {monsters[i].Name}의 공격!");
                Console.WriteLine($"{player.Name}을(를) 맞췄습니다.  [데미지 : {monsters[i].AttackPower}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine("HP 100 -> 94");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Thread.Sleep(1000);
            }

            int input = int.Parse(Console.ReadLine());

            switch ((BattleScene)input)
            {
                case BattleScene.Next:
                    ShowPlayerTurn();
                    break;
            }
        }

        // 배틀 결과
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
