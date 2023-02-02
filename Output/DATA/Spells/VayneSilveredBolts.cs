#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneSilveredBolts : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "VayneSilverBolts",
            BuffTextureName = "Vayne_SilveredBolts.dds",
            PersistsThroughDeath = true,
            SpellFXOverrideSkins = new[]{ "", },
            SpellToggleSlot = 2,
        };
        float[] effect0 = {0.04f, 0.05f, 0.06f, 0.07f, 0.08f};
        int[] effect1 = {20, 30, 40, 50, 60};
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            bool canMove; // UNUSED
            bool isBlinded; // UNITIALIZED
            canMove = GetCanMove(owner);
            if(!isBlinded)
            {
                returnValue = true;
            }
            else
            {
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        if(hitResult != HitResult.HIT_Dodge)
                        {
                            if(hitResult != HitResult.HIT_Miss)
                            {
                                int count;
                                count = GetBuffCountFromCaster(target, attacker, nameof(Buffs.VayneSilveredDebuff));
                                if(count == 2)
                                {
                                    TeamId teamID;
                                    TeamId teamIDTarget;
                                    Particle gragas; // UNUSED
                                    int level;
                                    float abilityPower;
                                    float bonusMaxHealthDamage;
                                    float tarMaxHealth;
                                    float rankScaling;
                                    float flatScaling;
                                    float damageToDeal;
                                    teamID = GetTeamID(attacker);
                                    teamIDTarget = GetTeamID(target);
                                    SpellEffectCreate(out gragas, out _, "vayne_W_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, default, default, target.Position, target, default, default, true, false, false, false, false);
                                    level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                    abilityPower = GetFlatMagicDamageMod(attacker);
                                    bonusMaxHealthDamage = 0 * abilityPower;
                                    SpellBuffClear(target, nameof(Buffs.VayneSilveredDebuff));
                                    tarMaxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                                    rankScaling = this.effect0[level];
                                    flatScaling = this.effect1[level];
                                    rankScaling += bonusMaxHealthDamage;
                                    damageToDeal = tarMaxHealth * rankScaling;
                                    damageToDeal += flatScaling;
                                    if(teamIDTarget == TeamId.TEAM_NEUTRAL)
                                    {
                                        damageToDeal = Math.Min(damageToDeal, 200);
                                    }
                                    ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                                }
                                else
                                {
                                    AddBuff(attacker, target, new Buffs.VayneSilveredDebuff(), 3, 1, 3.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}