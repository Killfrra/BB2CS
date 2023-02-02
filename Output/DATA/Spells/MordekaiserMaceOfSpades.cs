#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MordekaiserMaceOfSpades : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {25, 32, 39, 46, 53};
        int[] effect1 = {8, 7, 6, 5, 4};
        public override void SelfExecute()
        {
            float healthCost;
            float temp1;
            float nextBuffVars_SpellCooldown;
            healthCost = this.effect0[level];
            temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
            nextBuffVars_SpellCooldown = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.MordekaiserMaceOfSpades(nextBuffVars_SpellCooldown), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class MordekaiserMaceOfSpades : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "BUFFBONE_CSTM_WEAPON_1", "", },
            AutoBuffActivateEffect = new[]{ "", "mordakaiser_maceOfSpades_activate.troy", "", "", },
            BuffName = "MordekaiserMaceOfSpades",
            BuffTextureName = "MordekaiserMaceOfSpades.dds",
        };
        float spellCooldown;
        bool willRemove;
        int[] effect0 = {80, 110, 140, 170, 200};
        public MordekaiserMaceOfSpades(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            this.willRemove = false;
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is not ObjAIBase)
            {
            }
            else if(target is BaseTurret)
            {
            }
            else
            {
                TeamId teamID;
                float nextBuffVars_BaseDamage;
                int level;
                float baseDamage;
                float baseDamage;
                float totalDamage;
                float damageDifference;
                float bonusDamage;
                float abilityPower;
                float bonusAPDamage;
                float unitCount;
                teamID = GetTeamID(owner);
                this.willRemove = true;
                AddBuff((ObjAIBase)owner, owner, new Buffs.MordekaiserSyphonParticle(), 1, 1, 0.2f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                baseDamage = this.effect0[level];
                baseDamage = GetBaseAttackDamage(owner);
                totalDamage = GetTotalAttackDamage(owner);
                damageDifference = 0;
                if(damageAmount > totalDamage)
                {
                    damageDifference = damageAmount - totalDamage;
                }
                bonusDamage = totalDamage - baseDamage;
                baseDamage += bonusDamage;
                abilityPower = GetFlatMagicDamageMod(owner);
                bonusAPDamage = abilityPower * 0.4f;
                baseDamage += bonusAPDamage;
                unitCount = 0;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    unitCount++;
                }
                if(unitCount > 1)
                {
                    Particle a; // UNUSED
                    Vector3 targetPos;
                    SpellEffectCreate(out a, out _, "mordakaiser_maceOfSpades_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    targetPos = GetUnitPosition(target);
                    foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, target.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 4, default, true))
                    {
                        if(unit != target)
                        {
                            SpellCast((ObjAIBase)owner, unit, target.Position, target.Position, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, targetPos);
                        }
                    }
                }
                else
                {
                    Particle b; // UNUSED
                    baseDamage *= 1.65f;
                    SpellEffectCreate(out b, out _, "mordakaiser_maceOfSpades_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                if(damageDifference > 0)
                {
                    baseDamage += damageDifference;
                }
                damageAmount -= damageAmount;
                nextBuffVars_BaseDamage = baseDamage;
                AddBuff((ObjAIBase)target, owner, new Buffs.MordekaiserMaceOfSpadesDmg(nextBuffVars_BaseDamage), 1, 1, 0.001f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}