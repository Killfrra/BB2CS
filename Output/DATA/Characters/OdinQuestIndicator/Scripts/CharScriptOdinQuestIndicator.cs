﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOdinQuestIndicator : BBCharScript
    {
        public override void OnActivate()
        {
            SetMagicImmune(owner, true);
            SetPhysicalImmune(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            SetGhosted(owner, true);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectUseable, nameof(Buffs.OdinGuardianBuff), true))
            {
                AddBuff((ObjAIBase)unit, unit, new Buffs.OdinQuestIndicator(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinQuestParticleRemover(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}