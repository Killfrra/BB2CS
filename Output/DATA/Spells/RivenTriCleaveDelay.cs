#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenTriCleaveDelay : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        public override void OnActivate()
        {
            IncAcquisitionRangeMod(owner, -350);
        }
        public override void OnUpdateStats()
        {
            IncAcquisitionRangeMod(owner, -350);
        }
    }
}