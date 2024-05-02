namespace SpartaDungeonBattle
{
    public class ConsoleUtility
    {
        public static void PrintGameHeader()
        {
            Console.WriteLine("================================================================");
            Console.WriteLine("                                                                ");
            Console.WriteLine("    .d88888b                               dP                   ");
            Console.WriteLine("    88.                                    88                   ");
            Console.WriteLine("    `Y88888b. 88d888b. .d8888b. 88d888b. d8888P .d8888b.        ");
            Console.WriteLine("          `8b 88'  `88 88'  `88 88'  `88   88   88'  `88        ");
            Console.WriteLine("    d8'   .8P 88.  .88 88.  .88 88         88   88.  .88        ");
            Console.WriteLine("     Y88888P  88Y888P' `88888P8 dP         dP   `88888P8        ");
            Console.WriteLine("              88                                                ");
            Console.WriteLine("              dP                                                ");
            Console.WriteLine("888888ba                                                        ");
            Console.WriteLine("88    `8b                                                       ");
            Console.WriteLine("88     88 dP    dP 88d888b. .d8888b. .d8888b. .d8888b. 88d888b. ");
            Console.WriteLine("88     88 88    88 88'  `88 88'  `88 88ooood8 88'  `88 88'  `88 ");
            Console.WriteLine("88    .8P 88.  .88 88    88 88.  .88 88.  ... 88.  .88 88    88 ");
            Console.WriteLine("8888888P  `88888P' dP    dP `8888P88 `88888P' `88888P' dP    dP ");
            Console.WriteLine("                                 .88                            ");
            Console.WriteLine("                             d8888P                             ");
            Console.WriteLine("                                                                ");
            Console.WriteLine("================================================================");
            Console.WriteLine("                     PRESS ANYKEY TO START                      ");
            Console.WriteLine("================================================================");
            Console.ReadKey();
        }

        public static int PromptMenuChoice(int min, int max)
        {
            if (max > 0)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
            }

            while (true)
            {
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
            }
        }

        public static int PromptMenuChoice(int min, int max, List<Monster> monster)
        {
            Console.WriteLine("대상을 선택해주세요.");

            while (true)
            {
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    if (monster[choice - 1].IsDead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }
                    return choice;
                }
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
            }
        }

        public static void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        // 글자 색깔 설정
        public static void PrintTextHighlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }
        public static void PrintTextHighlights(string s1, string s2, string s3, string s4)
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s2);
            Console.ResetColor();
            Console.Write(s3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s4);
            Console.ResetColor();
        }

        public static void PrintAllTextHighlights(string s1, string s2, string s3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s1);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s3);
            Console.ResetColor();
        }


        // 글자수 확인
        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        // 글자수에 따라 패딩 지정
        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }
    }
}
