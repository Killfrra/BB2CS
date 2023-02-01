#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinCaptureChannelCooldownBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "OdinCaptureChannelCooldownBuff",
            BuffTextureName = "Odin_Interrupted.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}