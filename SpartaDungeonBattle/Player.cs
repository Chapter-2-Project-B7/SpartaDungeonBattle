using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeonBattle
{
    internal class Player
    {
        //직업 열거형
        enum CharacterClass : byte
        {
            Warrior
        }

        //생성자
        public Player(int level, string name, string playerClass, float attackDamage, float defense, float hp, int gold)
        {
            _level = level;
            _name = name;
            _playerClass = playerClass;
            _atk = attackDamage;
            _def = defense;
            _hp = hp;
            _gold = gold;
        }

        //레벨
        private int _level;
        public int level
        {
            get { return _level; }
            set { _level = value; }
        }

        //이름
        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        //캐릭터 직업
        private string _playerClass;
        public string playerClass
        {
            get { return _playerClass; }
            set { _playerClass = value; }
        }

        //공격력
        private float _atk;
        public float atk
        {
            get { return _atk; }
            set { _atk = value; }
        }

        //방어력
        private float _def;
        public float def
        {
            get { return _def; }
            set { _def = value; }
        }

        //체력
        private float _hp;
        public float hp
        {
            get { return hp; }
            set { hp = value; }
        }

        //골드
        private int _gold;
        public int gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
    }
}
