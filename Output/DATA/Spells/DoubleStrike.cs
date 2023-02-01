#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DoubleStrike : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "DoubleStrikeCharge",
            BuffTextureName = "DoubleStrike_Counter.dds",
        };
    }
}