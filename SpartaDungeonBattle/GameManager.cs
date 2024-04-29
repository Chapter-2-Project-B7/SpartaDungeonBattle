namespace SpartaDungeonBattle
{
    public class GameManager
    {
        public GameManager()
        {
            InitializeGame();
        }

        private void InitializeGame() { }

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

            int i = 0;

            switch (i)
            {
                case 1:
                    ShowStatusMenu();
                    break;

                case 2:
                    ShowBattleMenu();
                    break;

                case 0:
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
            Console.WriteLine("Lv.");
            Console.WriteLine("Chad ( 전사 )");
            Console.WriteLine("공격력 : ");
            Console.WriteLine("방어력 : ");
            Console.WriteLine("체 력 : ");
            Console.WriteLine("Gold : ");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int i = 0;

            switch (i)
            {
                case 0:
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
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.1 Chad (전사)");
            Console.WriteLine("HP 100/100");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int i = 0;

            switch (i)
            {
                case 0:
                    ShowMainMenu();
                    break;

                case 1:
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
            Console.WriteLine("Lv.1 Chad (전사)");
            Console.WriteLine("HP 100/100");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();

            int i = 0;

            switch (i)
            {
                case 0:
                    ShowBattleMenu();
                    break;

                // 해당 번호 몬스터 공격
            }
        }

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

            int i = 0;

            switch (i)
            {
                case 0:
                    ShowMonsterTurn();
                    break;
            }
        }

        private void ShowMonsterTurn()
        {
            Console.Clear();

            Console.WriteLine("Battle!!");
            Console.WriteLine();
            // 몬스터 공격 메세지 반복
            Console.WriteLine();
            Console.WriteLine("Lv.1 Chad");
            Console.WriteLine("HP 100 -> 94");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();

            int i = 0;

            switch (i)
            {
                case 1:
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
            Console.WriteLine("Lv.1  Chad");
            Console.WriteLine("HP 100 -> 74");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int i = 0;

            switch (i)
            {
                case 0:
                    ShowMainMenu();
                    break;
            }
        }
    }
}
