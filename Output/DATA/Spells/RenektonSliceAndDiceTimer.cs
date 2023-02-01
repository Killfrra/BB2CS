#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonSliceAndDiceTimer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "RenekthonSliceAndDiceDelay",
            BuffTextureName = "Renekton_SliceAndDiceDelay.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}