#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShrineBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "OdinShrineBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = true,
        };
        Particle buffParticle;
        float vampVar;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            this.vampVar = 0.3f;
            IncPercentLifeStealMod(owner, this.vampVar);
            IncPercentSpellVampMod(owner, this.vampVar);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, this.vampVar);
            IncPercentSpellVampMod(owner, this.vampVar);
        }
        public override void OnUpdateActions()
        {
            float sSCD1;
            float sSCD2;
            float newSSCD1;
            float newSSCD2;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                sSCD1 = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                sSCD2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                newSSCD1 = sSCD1 - 1;
                newSSCD2 = sSCD2 - 1;
                SetSlotSpellCooldownTimeVer2(newSSCD1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner, false);
                SetSlotSpellCooldownTimeVer2(newSSCD2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner, false);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float tempTable1_ThirdDA;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    SpellEffectCreate(out _, out _, "TiamatMelee_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false);
                    if(IsRanged(owner))
                    {
                        tempTable1_ThirdDA = 0.4f * damageAmount;
                    }
                    else
                    {
                        if(GetBuffCountFromCaster(owner, default, nameof(Buffs.JudicatorRighteousFury)) > 0)
                        {
                            tempTable1_ThirdDA = 0.4f * damageAmount;
                        }
                        else
                        {
                            tempTable1_ThirdDA = 0.6f * damageAmount;
                        }
                    }
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 210, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        if(target != unit)
                        {
                            if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
                            {
                                ApplyDamage((ObjAIBase)owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                            }
                            else if(damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
                            {
                                ApplyDamage((ObjAIBase)owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                            }
                            else
                            {
                                ApplyDamage((ObjAIBase)owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                            }
                        }
                    }
                }
            }
        }
    }
}