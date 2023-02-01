#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleGrenadeCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", "", },
            AutoBuffActivateEffect = new[]{ "Aegis_buf.troy", "", "", },
            BuffName = "RumbleGrenadeAmmo",
            BuffTextureName = "Rumble_Electro Harpoon.dds",
        };
    }
}