#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AuraofDespair : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Despair_buf.troy", "Despairpool_tar.troy", },
            BuffName = "AuraofDespair",
            BuffTextureName = "SadMummy_AuraofDespair.dds",
            SpellToggleSlot = 2,
        };
        float lastTimeExecuted;
        int[] effect0 = {8, 12, 16, 20, 24};
        int[] effect1 = {8, 12, 16, 20, 24};
        public override void OnActivate()
        {
            int level;
            float initialDamage;
            float abilityPowerMod;
            float abilityPowerBonus;
            float lifeLossPercent;
            float baseDamage;
            float temp1;
            float percentDamage;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            initialDamage = level * 0.003f;
            initialDamage += 0.012f;
            abilityPowerMod = GetFlatMagicDamageMod(owner);
            abilityPowerBonus = abilityPowerMod * 5E-05f;
            lifeLossPercent = abilityPowerBonus + initialDamage;
            baseDamage = this.effect0[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                temp1 = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                temp1 = Math.Min(temp1, 4500);
                percentDamage = temp1 * lifeLossPercent;
                percentDamage += baseDamage;
                ApplyDamage(attacker, unit, percentDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
            }
        }
        public override void OnUpdateActions()
        {
            int level;
            float ownerMana;
            float initialDamage;
            float abilityPowerMod;
            float abilityPowerBonus;
            float lifeLossPercent;
            float baseDamage;
            float temp1;
            float percentDamage;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ownerMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                if(ownerMana < 8)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    IncPAR(owner, -8, PrimaryAbilityResourceType.MANA);
                }
                initialDamage = level * 0.003f;
                initialDamage += 0.012f;
                abilityPowerMod = GetFlatMagicDamageMod(owner);
                abilityPowerBonus = abilityPowerMod * 5E-05f;
                lifeLossPercent = abilityPowerBonus + initialDamage;
                baseDamage = this.effect1[level];
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    temp1 = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    temp1 = Math.Min(temp1, 4500);
                    percentDamage = temp1 * lifeLossPercent;
                    percentDamage += baseDamage;
                    ApplyDamage(attacker, unit, percentDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
            }
        }
    }
}
namespace Spells
{
    public class AuraofDespair : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AuraofDespair)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.AuraofDespair), (ObjAIBase)owner);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AuraofDespair(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}