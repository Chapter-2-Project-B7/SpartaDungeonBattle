using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    public class Monster
    {
        private string name { get; set; }
        private int lv {  get; set; }
        private int hp { get; set; }

        public Monster(string name, int lv, int hp)
        {
            this.name = name;
            this.lv = lv;
            this.hp = hp;
        }

    }
}
