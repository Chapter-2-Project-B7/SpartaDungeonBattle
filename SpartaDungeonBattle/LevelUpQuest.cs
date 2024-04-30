﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    internal class LevelUpQuest : Quest
    {
        public LevelUpQuest() : base(
                                     "더욱더 성장하기",
                                     "우리는 성장하고 마을을 지켜주는 모험가에게 장비를 챙겨주고있네\n" +
                                     "성장해서 우리를 찾아오면 좋은 보상을 얻을 수 있을테야",
                                     "무기") { }


        public override void Request()
        {
            Console.WriteLine("- 레벨을 5까지 올리기!");
        }


        public override void CheckComplete()
        {
            //플레이어 레벨이 5가되면 보상지급
        }
    }
}
