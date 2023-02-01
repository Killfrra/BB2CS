#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonNoxianDiplomacyBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_Hand", "L_Hand", },
            AutoBuffActivateEffect = new[]{ "talon_Q_on_hit_ready_01.troy", "talon_Q_on_hit_ready_01.troy", },
            BuffName = "TalonNoxianDiplomacyBuff",
            BuffTextureName = "TalonNoxianDiplomacy.dds",
        };
    }
}