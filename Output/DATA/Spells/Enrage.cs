#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Enrage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon_tip", "", "", },
            AutoBuffActivateEffect = new[]{ "Enrageweapon_buf.troy", "", "", },
            BuffName = "Enrage",
            BuffTextureName = "Sion_SpiritRage.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        float bonusDamage;
        float bonusDamageIncrement;
        public Enrage(float bonusDamage = default, float bonusDamageIncrement = default)
        {
            this.bonusDamage = bonusDamage;
            this.bonusDamageIncrement = bonusDamageIncrement;
        }
        public override void OnActivate()
        {
            IncPermanentFlatPhysicalDamageMod(owner, this.bonusDamage);
        }
        public override void OnDeactivate(bool expired)
        {
            float bonusDamage;
            bonusDamage = this.bonusDamage * -1;
            IncPermanentFlatPhysicalDamageMod(owner, bonusDamage);
        }
        public override void OnKill()
        {
            int level;
            float passthrough;
            float hPGain;
            float nextBuffVars_HPGain;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            passthrough = level * 0.5f;
            hPGain = passthrough + 0.5f;
            nextBuffVars_HPGain = hPGain;
            AddBuff((ObjAIBase)owner, owner, new Buffs.EnrageMaxHP(nextBuffVars_HPGain), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            float healthOne;
            float healthCost;
            float currentHealth;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            healthOne = level * -2;
            healthCost = healthOne + -4;
            currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(currentHealth >= 15)
            {
                IncHealth(owner, healthCost, owner);
            }
            else
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 2)
            {
                IncPermanentFlatPhysicalDamageMod(owner, this.bonusDamageIncrement);
                this.bonusDamage += this.bonusDamageIncrement;
            }
        }
    }
}
namespace Spells
{
    public class Enrage : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {25, 35, 45, 55, 65};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_BonusDamage;
            float nextBuffVars_BonusDamageIncrement;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Enrage)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Enrage), (ObjAIBase)owner, 0);
            }
            else
            {
                nextBuffVars_BonusDamage = this.effect0[level];
                nextBuffVars_BonusDamageIncrement = 10;
                AddBuff(attacker, owner, new Buffs.Enrage(nextBuffVars_BonusDamage, nextBuffVars_BonusDamageIncrement), 1, 1, 20000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}