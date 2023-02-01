#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniFrostResistChaos : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Sejuani_Frost.troy", },
            BuffName = "SejuaniFrost",
            BuffTextureName = "Sejuani_Frost.dds",
        };
    }
}