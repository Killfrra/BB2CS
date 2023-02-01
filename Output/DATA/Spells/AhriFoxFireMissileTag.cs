#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AhriFoxFireMissileTag : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AhriFoxFire",
            BuffTextureName = "Ahri_FoxFire.dds",
        };
    }
}