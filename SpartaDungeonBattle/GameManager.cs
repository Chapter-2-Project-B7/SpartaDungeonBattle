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

        private void ShowInventoryMenu() { }

        private void ShowBattleMenu() { }
    }
}
