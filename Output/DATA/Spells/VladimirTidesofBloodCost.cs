#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VladimirTidesofBloodCost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VladimirTidesofBloodCost",
            BuffTextureName = "Vladimir_TidesofBlood.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentHPRegenMod(owner, 0.08f);
        }
    }
}