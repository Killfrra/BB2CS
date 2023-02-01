#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickRAPetSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserChildrenOftheGravePetSlow",
            BuffTextureName = "DarkChampioo_EndlessRage.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, -0.2f);
        }
    }
}