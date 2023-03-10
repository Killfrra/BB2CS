#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleCarpetBombCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "RumbleDangerZone",
            BuffTextureName = "Annie_GhastlyShield.dds",
            PersistsThroughDeath = true,
        };
    }
}