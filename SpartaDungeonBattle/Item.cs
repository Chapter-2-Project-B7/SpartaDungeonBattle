using System.Text.Json.Serialization;

namespace SpartaDungeonBattle
{
    public enum ItemType
    {
        WEAPON,
        ARMOR,
        POTION
    }

    public class Item
    {
        public string Name { get; }
        public string Desc { get; }

        public ItemType Type;

        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }

        public int Price { get; }

        public bool IsEquipped { get; private set; }
        public bool IsPurchased { get; private set; }

        public Item(
            string name,
            string desc,
            ItemType type,
            int atk,
            int def,
            int hp,
            int price,
            bool isEquipped = false,
            bool isPurchased = false
        )
        {
            Name = name;
            Desc = desc;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            Price = price;
            IsEquipped = isEquipped;
            IsPurchased = isPurchased;
        }

        public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 9));
            }
            else
                Console.Write(ConsoleUtility.PadRightForMixedText(Name, 12));

            Console.Write(" | ");

            if (Atk != 0)
                Console.Write($"공격력 {(Atk >= 0 ? "+" : "")}{Atk} ");
            if (Def != 0)
                Console.Write($"방어력 {(Atk >= 0 ? "+" : "")}{Def} ");
            if (Hp != 0)
                Console.Write($"체  력 {(Atk >= 0 ? "+" : "")}{Hp} ");

            Console.Write(" | ");

            Console.WriteLine(Desc);
        }

        public void PrintStoreItemDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");

            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }

            Console.Write(ConsoleUtility.PadRightForMixedText(Name, 12));

            Console.Write(" | ");

            if (Atk != 0)
                Console.Write($"공격력 {(Atk >= 0 ? "+" : "")}{Atk} ");
            if (Def != 0)
                Console.Write($"방어력 {(Def >= 0 ? "+" : "")}{Def} ");
            if (Hp != 0)
                Console.Write($"체  력 {(Hp >= 0 ? "+" : "")}{Hp}");

            Console.Write(" | ");

            Console.Write(ConsoleUtility.PadRightForMixedText(Desc, 12));

            Console.Write(" | ");

            if (IsPurchased)
            {
                Console.WriteLine("구매완료");
            }
            else
            {
                ConsoleUtility.PrintTextHighlights("", Price.ToString(), " G");
            }
        }

        public void ToggleEquipStatus()
        {
            IsEquipped = !IsEquipped;
        }

        public void Purchase()
        {
            IsPurchased = true;
        }

        public void UsePotion()
        {
            Console.WriteLine();
            if (Name == "체력 포션")
            {
                GameManager.Instance.player.HealthPoint += 50;
                Console.WriteLine($"{Name}을 사용하였습니다. 체력이 50 증가합니다.");
            }
            else if (Name == "마나 포션")
            {
                GameManager.Instance.player.ManaPoint += 10;
                Console.WriteLine($"{Name}을 사용하였습니다. 마나가 10 증가합니다.");
            }
            Thread.Sleep(1000);
        }
    }
}
