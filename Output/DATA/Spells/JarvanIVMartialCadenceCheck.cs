#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVMartialCadenceCheck : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVMartialCadenceCheck",
            BuffTextureName = "JarvanIV_MartialCadence.dds",
        };
    }
}