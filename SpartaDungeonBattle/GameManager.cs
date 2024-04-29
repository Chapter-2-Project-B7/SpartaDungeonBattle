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
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine();
        }

        private void ShowStatusMenu()
        {
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
        }

        private void ShowBattleMenu() { }
    }
}
