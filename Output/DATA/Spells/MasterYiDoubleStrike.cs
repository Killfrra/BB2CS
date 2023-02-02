﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MasterYiDoubleStrike : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            baseDamage = GetBaseAttackDamage(owner);
            if(target is ObjAIBase)
            {
                AddBuff(attacker, target, new Buffs.MasterYiDoubleStrike(), 1, 1, 0.15f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            else
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false);
            }
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false);
            RemoveOverrideAutoAttack(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.DoubleStrikeIcon), (ObjAIBase)owner);
        }
    }
}
namespace Buffs
{
    public class MasterYiDoubleStrike : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            if(owner.IsDead)
            {
            }
            else
            {
                float totalAttackDamage;
                totalAttackDamage = GetBaseAttackDamage(attacker);
                ApplyDamage(attacker, owner, totalAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false);
                if(target is ObjAIBase)
                {
                    SpellEffectCreate(out _, out _, "globalhit_yellow_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                }
            }
        }
    }
}