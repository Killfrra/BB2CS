#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonsterRetreatBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShadowWalk",
            BuffTextureName = "18.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.3f);
        }
    }
}