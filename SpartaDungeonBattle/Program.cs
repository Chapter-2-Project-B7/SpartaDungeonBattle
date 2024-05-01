﻿using System.Text.Json;

namespace SpartaDungeonBattle
{
    internal class Program
    {
        static void Main()
        {
            //GameManager.Instance.StartGame();
            QuestManager.Instance.InitQuest();

            QuestManager.Instance.SaveQuestData();
            QuestManager.Instance.LoadQuestData();
            //직렬화 테스트 코드
            /*QuestManager.Instance.InitQuest();
            

            
            string fileName = "Quests.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            Monster molnster = new Monster(1, "Slime", 0, 0);
            molnster.Die();
            QuestManager.Instance.EnterQuest();
            MonsterKillQuest? killQuest = QuestManager.Instance.quests[0] as MonsterKillQuest;
            string? jsonString = JsonSerializer.Serialize<MonsterKillQuest>(killQuest, options);

            //string jsonString = File.ReadAllText(fileName);

            //Quest? quest = JsonSerializer.Deserialize<Quest>(jsonString);
            Console.Write(jsonString);
        */
        }
    }
}
