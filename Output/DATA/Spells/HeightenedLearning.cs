#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HeightenedLearning : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Heightened Learning",
            BuffTextureName = "Chronokeeper_Slow.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentEXPBonus(owner, 0.08f);
        }
    }
}