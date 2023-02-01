#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzSeastoneTrident : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "root", },
            AutoBuffActivateEffect = new[]{ "Fizz_SeastoneTrident.troy", },
            BuffName = "FizzMalison",
            BuffTextureName = "FizzSeastoneActive.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int[] effect0 = {30, 40, 50, 60, 70};
        float[] effect1 = {0.04f, 0.05f, 0.06f, 0.07f, 0.08f};
        public override void OnUpdateActions()
        {
            int level;
            float baseMagic;
            float currentHealth;
            float maxHealth;
            float missingHealth;
            float basePercent;
            float baseAP;
            float flatAPBonus;
            float bonusDamage;
            float totalDamage;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                baseMagic = this.effect0[level];
                currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                missingHealth = maxHealth - currentHealth;
                basePercent = this.effect1[level];
                baseAP = GetFlatMagicDamageMod(attacker);
                flatAPBonus = baseAP * 0.35f;
                bonusDamage = missingHealth * basePercent;
                totalDamage = baseMagic + bonusDamage;
                totalDamage += flatAPBonus;
                totalDamage /= 6;
                if(owner is not Champion)
                {
                    totalDamage = Math.Min(totalDamage, 50);
                }
                ApplyDamage(attacker, owner, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
    }
}
namespace Spells
{
    public class FizzSeastoneTrident : BBSpellScript
    {
        public override void SelfExecute()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.FizzSeastoneTridentActive(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}