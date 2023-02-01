#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ScurvyStrikeParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "Global_Slow.troy", },
            BuffName = "ScurvyStrike",
            BuffTextureName = "Pirate_GrogSoakedBlade.dds",
        };
    }
}