using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public class MonsterKillQuest : Quest
    {
        public MonsterKillQuest() : base(
                                    "마을을 위협하는 몬스터 처치",
                                    "이봐! 마을 근처에 몬스터들이 너무 많아졌다고 생각하지 않나?\n" +
                                    "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                                    "모험가인 자네가 좀 처치해주게!",
                                    "쓸만한 방패"){}

        override public void Request()
        {
            Console.WriteLine("- 슬라임 5마리 처치 (0/5)");
        }
    }
}
