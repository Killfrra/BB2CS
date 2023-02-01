#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleGrenadeCD : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RumbleGrenadeAmmo",
            BuffTextureName = "Heimerdinger+HextechMicroRockets.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
    }
}