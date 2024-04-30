using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    internal class Thor : Monster
    {

        public int Mp;
        public string SkillName { get; }
        public int SkillPower { get; }

        public Thor() : base(10, "Thor", 10, 100, false)
        {
            Mp = 100;
            SkillName = "전격";
            SkillPower = 15;
        }

       
        
        public void UseSkill()
        {
            Console.WriteLine($"{Name}이(가) {SkillName} 시전!");
            Console.WriteLine($"{SkillName}이(가) {SkillPower} 데미지를 플레이어에게 주었습니다.");
            //플레이어 데미지 입음
        }

        
    }
}
