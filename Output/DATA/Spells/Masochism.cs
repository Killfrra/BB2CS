#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Masochism : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_hand", "L_hand", "", "", },
            AutoBuffActivateEffect = new[]{ "dr_mundo_masochism_cas.troy", "dr_mundo_masochism_cas.troy", },
            BuffName = "Masochism",
            BuffTextureName = "DrMundo_Masochism.dds",
            IsDeathRecapSource = true,
        };
        float damageMod;
        float baseIncrease;
        public Masochism(float damageMod = default, float baseIncrease = default)
        {
            this.damageMod = damageMod;
            this.baseIncrease = baseIncrease;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageMod);
            //RequireVar(this.baseIncrease);
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            float baseIncrease;
            float damageMod;
            float health;
            float healthMissing;
            float rawDamage;
            float damageBonus;
            baseIncrease = this.baseIncrease;
            damageMod = this.damageMod;
            health = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            healthMissing = 1 - health;
            rawDamage = 100 * healthMissing;
            damageBonus = damageMod * rawDamage;
            IncFlatPhysicalDamageMod(owner, damageBonus);
            IncFlatPhysicalDamageMod(owner, baseIncrease);
        }
    }
}
namespace Spells
{
    public class Masochism : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {25, 35, 45, 55, 65};
        int[] effect1 = {-25, -35, -45, -55, -65};
        float[] effect2 = {0.4f, 0.55f, 0.7f, 0.85f, 1};
        int[] effect3 = {40, 55, 70, 85, 100};
        public override bool CanCast()
        {
            bool returnValue = true;
            int healthCost;
            float temp1;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                healthCost = this.effect0[level];
                temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if(temp1 >= healthCost)
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            float healthCost;
            float nextBuffVars_DamageMod;
            int nextBuffVars_BaseIncrease;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            healthCost = this.effect1[level];
            nextBuffVars_DamageMod = this.effect2[level];
            nextBuffVars_BaseIncrease = this.effect3[level];
            IncHealth(owner, healthCost, owner);
            AddBuff(attacker, target, new Buffs.Masochism(nextBuffVars_DamageMod, nextBuffVars_BaseIncrease), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}