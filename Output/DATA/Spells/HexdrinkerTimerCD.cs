#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HexdrinkerTimerCD : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "HexdrunkTimerCD",
            BuffTextureName = "3155_Hexdrinker_Inactive.dds",
            PersistsThroughDeath = true,
        };
    }
}