#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrgotSwapTarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "UrgotSwapTarget",
            BuffTextureName = "UrgotPositionReverser.dds",
        };
        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
        }
    }
}