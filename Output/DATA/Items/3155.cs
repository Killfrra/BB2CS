#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3155 : BBItemScript
    {
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            TeamId teamID;
            float hP;
            float projectedHP;
            float maxHP;
            float newPercentHP;
            Particle c; // UNUSED
            Particle a; // UNUSED
            Particle b; // UNUSED
            float shieldHealth;
            float nextBuffVars_ShieldHealth;
            teamID = GetTeamID(owner);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HexdrinkerTimer)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Hexdrinker)) == 0)
                {
                    if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
                    {
                        if(damageAmount > 0)
                        {
                            hP = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                            projectedHP = hP - damageAmount;
                            maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                            newPercentHP = projectedHP / maxHP;
                            if(newPercentHP < 0.3f)
                            {
                                SpellEffectCreate(out c, out _, "hexTech_dmg_shield_birth.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                                SpellEffectCreate(out a, out _, "hexTech_dmg_shield_onHit_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                                SpellEffectCreate(out b, out _, "hexTech_dmg_shield_onHit_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                                shieldHealth = 300;
                                if(shieldHealth >= damageAmount)
                                {
                                    shieldHealth -= damageAmount;
                                    nextBuffVars_ShieldHealth = shieldHealth;
                                    damageAmount = 0;
                                    AddBuff((ObjAIBase)owner, owner, new Buffs.Hexdrinker(nextBuffVars_ShieldHealth), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                                    AddBuff((ObjAIBase)owner, owner, new Buffs.HexdrinkerTimer(), 1, 1, 60, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                                }
                                else
                                {
                                    damageAmount -= shieldHealth;
                                    nextBuffVars_ShieldHealth = 0;
                                    AddBuff((ObjAIBase)owner, owner, new Buffs.Hexdrinker(nextBuffVars_ShieldHealth), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                                    AddBuff((ObjAIBase)owner, owner, new Buffs.HexdrinkerTimer(), 1, 1, 60, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}