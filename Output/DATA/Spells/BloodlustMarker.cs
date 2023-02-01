#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BloodlustMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Blood Lust",
            BuffTextureName = "DarkChampion_Bloodlust.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int[] effect0 = {5, 10, 15, 20, 25};
        int[] effect1 = {15, 20, 25, 30, 35};
        public override void OnUpdateStats()
        {
            int level;
            float baseDmg;
            float healthPercent;
            float missingPercent;
            float dmgPerMissingHealth;
            float variableDmg;
            float totalBonusDmg;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDmg = this.effect0[level];
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            missingPercent = 1 - healthPercent;
            dmgPerMissingHealth = this.effect1[level];
            variableDmg = dmgPerMissingHealth * missingPercent;
            totalBonusDmg = variableDmg + baseDmg;
            IncFlatPhysicalDamageMod(owner, totalBonusDmg);
        }
    }
}