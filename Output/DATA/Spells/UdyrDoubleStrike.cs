﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrDoubleStrike : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            float totalAttackDamage;
            if(owner.IsDead)
            {
            }
            else
            {
                totalAttackDamage = GetBaseAttackDamage(attacker);
                ApplyDamage(attacker, owner, totalAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 0.33f, 0, default, false, false);
                ApplyDamage(attacker, owner, totalAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 0.33f, 0, default, false, false);
                if(target is ObjAIBase)
                {
                    SpellEffectCreate(out _, out _, "globalhit_yellow_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class UdyrDoubleStrike : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            baseDamage = GetBaseAttackDamage(owner);
            if(target is ObjAIBase)
            {
                AddBuff(attacker, target, new Buffs.UdyrDoubleStrike(), 1, 1, 0.15f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
            else
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 0.33f, 0, default, false, false);
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 0.33f, 0, default, false, false);
            }
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 0.33f, 0, default, false, false);
        }
    }
}