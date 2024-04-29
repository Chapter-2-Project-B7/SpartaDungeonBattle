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
        public string Name { get; set; }
        public int Lv { get; set; }
        public int Hp { get; set; }
        public int AttackPower { get; set; }
        public bool IsDead { get; set; }

        public Monster(string name, int lv, int attackPower, int hp)
        {
            Name = name;
            Lv = lv;
            Hp = hp;
            AttackPower = attackPower;
            IsDead = false;
        }

        void TakeDamage(int damage)
        {
            if (IsDead)
            {
                Console.WriteLine("이미 죽은 몬스터입니다.");
                return;
            }
            else
            {
                Hp -= damage;
                if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
                else Console.WriteLine($"{Name}이(가) {damage}만큼 피해를 입었습니다. 남은 체력 {Hp}");
            }
        }
    }
}
