﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class XenZhaoThrust2 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {15, 30, 45, 60, 75};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float attackDmg;
            float combo1DamageLeet;
            float combo1Damage;
            Particle bye; // UNUSED
            if(hitResult == HitResult.HIT_Dodge)
            {
                hitResult = HitResult.HIT_Normal;
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            attackDmg = GetTotalAttackDamage(owner);
            combo1DamageLeet = this.effect0[level];
            combo1Damage = combo1DamageLeet + attackDmg;
            SpellEffectCreate(out bye, out _, "xenZiou_ChainAttack_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
            if(hitResult == HitResult.HIT_Critical)
            {
                float comboDamageCrit;
                comboDamageCrit = attackDmg * 2;
                combo1Damage = combo1DamageLeet + comboDamageCrit;
            }
            ApplyDamage(attacker, target, combo1Damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            SpellBuffRemove(attacker, nameof(Buffs.XenZhaoComboAuto), attacker);
            AddBuff(attacker, attacker, new Buffs.XenZhaoComboAutoFinish(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            IssueOrder(attacker, OrderType.AttackTo, default, target);
        }
    }
}