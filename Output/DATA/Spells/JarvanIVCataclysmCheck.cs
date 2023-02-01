#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVCataclysmCheck : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVMartialCadenceCheck",
            BuffTextureName = "JarvanIV_Cataclysm.dds",
        };
    }
}