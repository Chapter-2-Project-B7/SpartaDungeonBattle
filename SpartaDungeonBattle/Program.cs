namespace SpartaDungeonBattle
{
    internal class Program
    {
        static void Main()
        {
            /*GameManager gameManager = new GameManager();
            gameManager.StartGame();*/

            QuestManager questManager = new QuestManager();
            questManager.InitQuest();
            questManager.EnterQuest();
        }
    }
}
