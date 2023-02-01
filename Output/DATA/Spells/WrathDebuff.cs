#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WrathDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Wrath of the Ancients",
            BuffTextureName = "PlantKing_AnimateEntangler.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatMagicReduction(owner, -15);
        }
    }
}