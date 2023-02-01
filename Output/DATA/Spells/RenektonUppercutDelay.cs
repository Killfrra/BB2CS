﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonUppercutDelay : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            if(owner.IsDead)
            {
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.RenektonUppercut(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                if(target is ObjAIBase)
                {
                    SpellEffectCreate(out _, out _, "globalhit_yellow_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                }
            }
            SetCanCast(owner, true);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}