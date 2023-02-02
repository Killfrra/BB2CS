#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BurningAgony : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {20, 25, 30, 35, 40};
        public override bool CanCast()
        {
            bool returnValue = true;
            float health1;
            health1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                float healthCost;
                healthCost = this.effect0[level];
                if(healthCost >= health1)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BurningAgony)) > 0)
            {
            }
            else
            {
                returnValue = 0;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BurningAgony)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.BurningAgony), (ObjAIBase)owner, 0);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.BurningAgony(), 1, 1, 30000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class BurningAgony : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "root", },
            AutoBuffActivateEffect = new[]{ "dr_mundo_burning_agony_cas_02.troy", "", },
            BuffName = "BurningAgony",
            BuffTextureName = "DrMundo_BurningAgony.dds",
            SpellToggleSlot = 2,
        };
        float lastTimeExecuted;
        float[] effect0 = {0.85f, 0.8f, 0.75f, 0.7f, 0.65f};
        int[] effect1 = {40, 55, 70, 85, 100};
        int[] effect2 = {10, 15, 20, 25, 30};
        int[] effect3 = {40, 55, 70, 85, 100};
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            int level;
            float durationMod;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            durationMod = this.effect0[level];
            if(owner.Team != attacker.Team)
            {
                float percentReduction; // UNITIALIZED
                if(type == BuffType.SNARE)
                {
                    duration *= durationMod;
                }
                if(type == BuffType.SLOW)
                {
                    duration *= durationMod;
                }
                if(type == BuffType.FEAR)
                {
                    duration *= durationMod;
                }
                if(type == BuffType.CHARM)
                {
                    duration *= durationMod;
                }
                if(type == BuffType.SLEEP)
                {
                    duration *= durationMod;
                }
                if(type == BuffType.STUN)
                {
                    duration *= durationMod;
                }
                if(type == BuffType.TAUNT)
                {
                    duration *= durationMod;
                }
                if(type == BuffType.SILENCE)
                {
                    duration *= percentReduction;
                }
                if(type == BuffType.BLIND)
                {
                    duration *= percentReduction;
                }
                duration = Math.Max(0.3f, duration);
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            int level;
            float damageToDeal;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damageToDeal = this.effect1[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                int level;
                float healthCost;
                float health;
                float damageToDeal;
                float damageToDealSelf;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                healthCost = this.effect2[level];
                health = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                damageToDeal = this.effect3[level];
                damageToDealSelf = -1 * healthCost;
                if(health < healthCost)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    IncHealth(owner, damageToDealSelf, owner);
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}