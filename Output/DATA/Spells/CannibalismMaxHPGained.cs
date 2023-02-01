#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CannibalismMaxHPGained : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CannibalismMaxHPGained",
            BuffTextureName = "Sion_SpiritFeast.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float cannibalismMaxHPMod;
        public CannibalismMaxHPGained(float cannibalismMaxHPMod = default)
        {
            this.cannibalismMaxHPMod = cannibalismMaxHPMod;
        }
        public override void OnActivate()
        {
            charVars.CannibalismMaxHPMod += this.cannibalismMaxHPMod;
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, charVars.CannibalismMaxHPMod);
        }
    }
}