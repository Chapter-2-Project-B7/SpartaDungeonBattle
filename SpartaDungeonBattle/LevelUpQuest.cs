using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public class LevelUpQuest : Quest
    {
        public int targetLevel { get; set; }
        public LevelUpQuest() : base(
                                     "더욱더 성장하기",
                                     "우리는 성장하고 마을을 지켜주는 모험가에게 장비를 챙겨주고있네\n" +
                                     "성장해서 우리를 찾아오면 좋은 보상을 얻을 수 있을테야",
                                     new Item("롱 소드", "한손으로 들기 버거운 무기이다.", ItemType.ARMOR, 4, 0, 0, 1000))
        {
            targetLevel = 5;
        }

        public override void Request()
        {
            Console.WriteLine("- 레벨을 5까지 올리기!");
            Console.WriteLine($"플레이어 레벨 : {GameManager.Instance.player.Level}");
        }


        public override void CheckComplete()
        {
            if (GameManager.Instance.player.Level >= targetLevel)
                this.CompleteQuest();
        }
    }
}
