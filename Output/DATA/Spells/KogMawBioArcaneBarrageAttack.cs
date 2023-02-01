#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KogMawBioArcaneBarrageAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.02f, 0.03f, 0.04f, 0.05f, 0.06f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            int kMSkinID;
            Particle a; // UNUSED
            float abilityPower;
            float maxHealthDamage;
            float bonusMaxHealthDamage;
            float totalMaxHealthDamage;
            float maxHealth;
            float damageToApply;
            TeamId teamId;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = GetBaseAttackDamage(owner);
            kMSkinID = GetSkinID(attacker);
            if(target is ObjAIBase)
            {
                if(kMSkinID == 5)
                {
                    SpellEffectCreate(out a, out _, "KogMawChineseBasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "KogMawSpatter.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
            }
            ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(hitResult != HitResult.HIT_Dodge)
                    {
                        if(hitResult != HitResult.HIT_Miss)
                        {
                            abilityPower = GetFlatMagicDamageMod(owner);
                            maxHealthDamage = this.effect0[level];
                            bonusMaxHealthDamage = 0.0001f * abilityPower;
                            totalMaxHealthDamage = bonusMaxHealthDamage + maxHealthDamage;
                            maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                            damageToApply = maxHealth * totalMaxHealthDamage;
                            teamId = GetTeamID(target);
                            if(teamId == TeamId.TEAM_NEUTRAL)
                            {
                                damageToApply = Math.Min(100, damageToApply);
                            }
                            ApplyDamage(attacker, target, damageToApply, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                        }
                    }
                }
            }
        }
    }
}