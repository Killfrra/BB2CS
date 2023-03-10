#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleHeatingUp : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", "", },
            AutoBuffActivateEffect = new[]{ "Aegis_buf.troy", "", "", },
            BuffName = "Heating Up",
            BuffTextureName = "034_Steel_Shield.dds",
        };
    }
}