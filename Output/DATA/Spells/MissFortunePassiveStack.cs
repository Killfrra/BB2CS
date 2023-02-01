#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MissFortunePassiveStack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MissFortunePassiveStack",
            BuffTextureName = "MissFortune_ImpureShots.dds",
        };
    }
}