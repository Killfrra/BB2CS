#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VeigarEquilibrium : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VeigarEquilibrium",
            BuffTextureName = "VeigarEntropy.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            float percentMana;
            float percentMissing;
            percentMana = GetPARPercent(owner, PrimaryAbilityResourceType.MANA);
            percentMissing = 1 - percentMana;
            percentMissing *= 0.75f;
            IncPercentPARRegenMod(owner, percentMissing, PrimaryAbilityResourceType.MANA);
        }
    }
}