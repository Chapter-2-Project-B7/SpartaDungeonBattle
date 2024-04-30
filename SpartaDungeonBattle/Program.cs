namespace SpartaDungeonBattle
{
    internal class Program
    {
        static void Main()
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();


            //몬스터 처치 퀘스트 테스트 코드
            /*Monster monster = new Monster("Slime", 1, 1, 1);
            QuestManager.Instance.InitQuest();
            QuestManager.Instance.EnterQuest();

            for (int i = 0; i < QuestManager.Instance.quests.Count; i++)
            {
                monster.MonsterDied += QuestManager.Instance.quests[i].HandleMonsterDied;
            }
            


            monster.Die();
            monster.Die();
            monster.Die();
            monster.Die();
            monster.Die();
            QuestManager.Instance.EnterQuest();*/

        }
    }
}
