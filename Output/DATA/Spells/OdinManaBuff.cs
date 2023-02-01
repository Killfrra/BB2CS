#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinManaBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "OdinManaBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int cooldownVar; // UNUSED
        public override void OnActivate()
        {
            this.cooldownVar = 0;
        }
        public override void OnUpdateStats()
        {
            int level; // UNUSED
            float percentMana;
            float percentMissing;
            level = GetLevel(owner);
            percentMana = GetPARPercent(owner, PrimaryAbilityResourceType.MANA);
            percentMissing = 1 - percentMana;
            percentMissing *= 2.1f;
            IncPercentPARRegenMod(owner, percentMissing, PrimaryAbilityResourceType.MANA);
            IncPercentArmorPenetrationMod(owner, 0.15f);
            IncPercentMagicPenetrationMod(owner, 0.05f);
        }
    }
}