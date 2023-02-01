﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOdin_Red_Minion_Caster : BBCharScript
    {
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}