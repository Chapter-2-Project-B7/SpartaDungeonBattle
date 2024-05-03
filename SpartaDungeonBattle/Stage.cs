﻿namespace SpartaDungeonBattle
{
    public class Stage
    {
        public Player Player;
        public List<Monster> Monsters;
        public List<Monster> RandomMonsters;
        public List<Item> ClearItemList;

        private Random rand = new Random();
        private int startHP;

        public delegate void StageEvent(Character character);
        public event StageEvent OnCharacterDeath;

        public Stage(
            Player player,
            List<Monster> monsters,
            List<Monster> randomMonsters,
            List<Item> clearItemList
        )
        {
            Player = player;
            Monsters = monsters;
            RandomMonsters = randomMonsters;
            ClearItemList = clearItemList;

            OnCharacterDeath += StageClear;
        }

        private void GenerateMonsterList()
        {
            RandomMonsters.Clear();

            int maxList = (int)Math.Ceiling((float)(GameManager.Instance.stageCount + 4) / 2);

            for (int i = 0; i < maxList; i++)
            {
                int startIdx = (int)(Math.Ceiling((float)Player.Level / 2) - 1);

                int idx = rand.Next(startIdx, Monsters.Count);

                RandomMonsters.Add(
                    new Monster(
                        Monsters[idx].Level,
                        Monsters[idx].Name,
                        Monsters[idx].AttackPower,
                        Monsters[idx].HealthPoint
                    )
                );
            }
        }

        private void PrintMonsterList(bool withNumber = false)
        {
            foreach (Monster monster in RandomMonsters)
            {
                if (monster.IsDead == true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    if (withNumber)
                    {
                        Console.Write($"{RandomMonsters.IndexOf(monster) + 1} ");
                    }
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} Dead");
                    Console.ResetColor();
                }
                else
                {
                    if (withNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"{RandomMonsters.IndexOf(monster) + 1} ");
                        Console.ResetColor();
                    }
                    ConsoleUtility.PrintTextHighlights(
                        "Lv.",
                        $"{monster.Level} ",
                        $"HP ",
                        $"{monster.HealthPoint}"
                    );
                    Console.WriteLine($" {monster.Name}");
                }
            }
        }

        private bool CheckAllMonstersAreDead(List<Monster> randomMonsters)
        {
            if (randomMonsters.All(monster => monster.IsDead))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StageStart()
        {
            GenerateMonsterList();
            ShowBattleMenu();
        }

        private void ShowBattleMenu()
        {
            ClearItemList.Clear();

            startHP = Player.HealthPoint;

            Console.Clear();
            ConsoleUtility.ShowTitle("Battle!!");
            Console.WriteLine();

            PrintMonsterList();

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            ConsoleUtility.PrintTextHighlights(
                "Lv.",
                $"{Player.Level}  ",
                $"{Player.Name} ({Player.Job})"
            );
            Console.Write("HP ");
            ConsoleUtility.PrintAllTextHighlights($"{Player.HealthPoint} ", "/ ", $"{Player.MaxHealthPoint}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 1);

            switch ((BattleMenu)choice)
            {
                case BattleMenu.Exit:
                    GameManager.Instance.ShowMainMenu();
                    break;

                case BattleMenu.Attack:
                    ShowSelectMonster();
                    break;
            }
        }

        private void ShowSelectMonster()
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("Battle!!");
            Console.WriteLine();

            PrintMonsterList(true);

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            ConsoleUtility.PrintTextHighlights(
                "Lv.",
                $"{Player.Level}  ",
                $"{Player.Name} ({Player.Job})"
            );
            Console.Write("HP ");
            ConsoleUtility.PrintAllTextHighlights($"{Player.HealthPoint} ", "/ ", $"{Player.MaxHealthPoint}");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(1, RandomMonsters.Count, RandomMonsters);

            switch (choice)
            {
                default:
                    ShowPlayerPhase(choice);
                    break;
            }
        }

        private void ShowPlayerPhase(int monsterNum)
        {
            Monster monster = RandomMonsters[monsterNum - 1];

            Console.Clear();
            ConsoleUtility.ShowTitle("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{Player.Name} 의 공격!");

            var damageResult = Player.CalculateDamage();
            monster.TakeDamage(damageResult.Item1, damageResult.Item2);

            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int choice = ConsoleUtility.PromptMenuChoice(0, 0);

            switch ((BattlePhase)choice)
            {
                case BattlePhase.Next:
                    if (CheckAllMonstersAreDead(RandomMonsters))
                    {
                        OnCharacterDeath?.Invoke(monster);
                        break;
                    }
                    else
                    {
                        ShowMonsterPhase();
                        break;
                    }
            }
        }

        private void ShowMonsterPhase()
        {
            for (int i = 0; i < RandomMonsters.Count; i++)
            {
                Monster monster = RandomMonsters[i];

                if (monster.IsDead)
                {
                    continue;
                }
                else
                {
                    Console.Clear();
                    ConsoleUtility.ShowTitle("Battle!!");
                    Console.WriteLine();
                    ConsoleUtility.PrintTextHighlights(
                        "Lv.",
                        $"{monster.Level} ",
                        $"{monster.Name} 의 공격!"
                    );

                    var damageResult = monster.CalculateDamage();
                    Player.TakeDamage(damageResult.Item1, damageResult.Item2);
                    Thread.Sleep(1000);

                    if (Player.IsDead)
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
                    if (Player.IsDead)
                    {
                        OnCharacterDeath?.Invoke(Player);
                        break;
                    }
                    else
                    {
                        ShowSelectMonster();
                        break;
                    }
            }
        }

        public void StageClear(Character character)
        {
            if (character is Monster)
            {
                int currentLevel = Player.Level;
                int currentEXP = Player.CurrentExp;
                int gainEXP = 0;

                for (int i = 0; i < RandomMonsters.Count; i++)
                {
                    gainEXP += RandomMonsters[i].Level;
                }

                Console.Clear();
                ConsoleUtility.ShowTitle("Battle!! - Result");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Victory");
                Console.ResetColor();
                Console.WriteLine();
                ConsoleUtility.PrintTextHighlights(
                    "던전에서 몬스터 ",
                    $"{RandomMonsters.Count}",
                    "마리를 잡았습니다."
                );
                Console.WriteLine();
                Console.WriteLine("[캐릭터 정보]");
                Console.Write("Lv.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{currentLevel}");
                Console.ResetColor();
                Console.Write($" {Player.Name}");

                Player.GetExp(gainEXP);
                Console.WriteLine();
                Console.Write("HP ");
                ConsoleUtility.PrintAllTextHighlights($"{startHP}", " -> ", $"{Player.HealthPoint}");
                Console.Write("exp ");
                ConsoleUtility.PrintAllTextHighlights(
                    $"{currentEXP}",
                    " -> ",
                    $"{currentEXP + gainEXP}"
                );
                Console.WriteLine();
                Console.WriteLine("[획득 아이템]");
                foreach (Item item in ClearItemList)
                {
                    Console.WriteLine($"{item.Name}");

                    if (item.Type == ItemType.POTION)
                        GameManager.Instance.inventory.potionInventory.Add(item);
                    else
                        GameManager.Instance.inventory.equipInventory.Add(item);
                }
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();

                GameManager.Instance.stageCount += 1;

                int choice = ConsoleUtility.PromptMenuChoice(0, 0);

                switch ((BattlePhase)choice)
                {
                    case BattlePhase.Next:
                        GameManager.Instance.ShowMainMenu();
                        break;
                }
            }
            else
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("Battle!! - Result");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You Lose");
                Console.ResetColor();
                Console.WriteLine();
                ConsoleUtility.PrintTextHighlights("Lv.", $"{Player.Level} ", $"{Player.Name}");
                Console.Write("HP ");
                ConsoleUtility.PrintAllTextHighlights($"{startHP}", " -> ", $"0");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();

                Player.IsDead = false;

                int choice = ConsoleUtility.PromptMenuChoice(0, 0);

                switch ((BattlePhase)choice)
                {
                    case BattlePhase.Next:
                        GameManager.Instance.ShowMainMenu();
                        break;
                }
            }
        }
    }
}
