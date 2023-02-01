#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenTriCleaveGhost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        public override void OnActivate()
        {
            SetGhosted(owner, true);
            IncAcquisitionRangeMod(owner, -600);
        }
        public override void OnUpdateActions()
        {
            SetGhosted(owner, true);
            IncAcquisitionRangeMod(owner, -600);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
            IncAcquisitionRangeMod(owner, 0);
        }
    }
}