﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RedCardAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        int[] effect0 = {30, 45, 60, 75, 90};
        float[] effect1 = {-0.3f, -0.35f, -0.4f, -0.45f, -0.5f};
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.PickACard), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.RedCardPreAttack), (ObjAIBase)owner);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float attackDamage;
            float bonusDamage;
            float redCardDamage;
            Particle arrm8y; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            teamID = GetTeamID(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            attackDamage = GetTotalAttackDamage(owner);
            bonusDamage = this.effect0[level];
            redCardDamage = attackDamage + bonusDamage;
            if(target is ObjAIBase)
            {
                ApplyDamage(attacker, target, 0, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                SpellEffectCreate(out arrm8y, out _, "PickaCard_red_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                if(target is BaseTurret)
                {
                    ApplyDamage(attacker, target, redCardDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                }
            }
            else
            {
                float baseDamage;
                Vector3 targetPosition; // UNITIALIZED
                baseDamage = GetBaseAttackDamage(attacker);
                SpellEffectCreate(out arrm8y, out _, "PickaCard_red_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPosition, default, default, targetPosition, false, default, default, false);
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            }
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, target.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, redCardDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.CardmasterSlow(nextBuffVars_MoveSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}