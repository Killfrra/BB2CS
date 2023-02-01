#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniWintersClawBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "SejuaniWintersClawBuff",
            BuffTextureName = "Voidwalker_NullBlade.dds",
            SpellToggleSlot = 2,
        };
    }
}