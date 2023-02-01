#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OlafBerzerkerRage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OlafBerzerkerRage",
            BuffTextureName = "OlafBerserkerRage.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            float healthPerc;
            float aSPerc;
            healthPerc = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            aSPerc = 1 - healthPerc;
            IncPercentAttackSpeedMod(owner, aSPerc);
        }
    }
}