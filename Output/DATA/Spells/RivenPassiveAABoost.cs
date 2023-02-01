#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenPassiveAABoost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "r_hand", "", },
            AutoBuffActivateEffect = new[]{ "exile_passive_buf.troy", "", },
            BuffName = "RivenPassiveAABoost",
            BuffTextureName = "RivenRunicBlades.dds",
        };
    }
}