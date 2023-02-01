#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyDefenseParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "L_finger", },
            AutoBuffActivateEffect = new[]{ "PoppyDef_max.troy", },
            BuffTextureName = "",
        };
    }
}