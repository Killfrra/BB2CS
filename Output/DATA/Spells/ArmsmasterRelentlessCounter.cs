#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ArmsmasterRelentlessCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RelentlessCounter",
            BuffTextureName = "Armsmaster_CoupDeGrace.dds",
        };
    }
}