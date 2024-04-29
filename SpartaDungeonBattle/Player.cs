﻿using System;
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
        public Player(int level, string name, string job, float atk, float def, float hp, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }

        //레벨
        public int Level { get; }

        //이름
        public string Name { get; }

        //캐릭터 직업
        public string Job { get; }

        //공격력
        public float Atk { get; }

        //방어력
        public float Def { get; }

        //체력
        public float Hp { get; set; }

        //골드
        public int Gold { get; }
    }
}