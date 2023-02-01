#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearWParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "C_BUFFBONE_GLB_CENTER_LOC", "L_BUFFBONE_GLB_HAND_LOC", "R_BUFFBONE_GLB_HAND_LOC", },
            AutoBuffActivateEffect = new[]{ "Volibear_maxStack_indicator.troy", "Volibear_maxStack_indicator_cast.troy", "Volibear_maxStack_indicator_cast.troy", "", },
        };
    }
}