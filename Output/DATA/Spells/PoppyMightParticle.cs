#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyMightParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "hammer_b", },
            AutoBuffActivateEffect = new[]{ "PoppyDam_max.troy", },
            BuffTextureName = "BlindMonk_BlindingStrike.dds",
        };
    }
}