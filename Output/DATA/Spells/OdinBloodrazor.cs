﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinBloodrazor : BBBuffScript
    {
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float maxHealth;
            TeamId teamId;
            float damage;
            ObjAIBase caster;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(hitResult != HitResult.HIT_Dodge)
                    {
                        if(hitResult != HitResult.HIT_Miss)
                        {
                            maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                            teamId = GetTeamID(target);
                            damage = 0.025f * maxHealth;
                            if(teamId == TeamId.TEAM_NEUTRAL)
                            {
                                damage = Math.Min(120, damage);
                            }
                            caster = SetBuffCasterUnit();
                            if(attacker is not Champion)
                            {
                                caster = GetPetOwner((Pet)attacker);
                            }
                            ApplyDamage(caster, target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                        }
                    }
                }
            }
        }
    }
}