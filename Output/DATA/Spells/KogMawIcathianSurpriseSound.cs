#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KogMawIcathianSurpriseSound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KogMawIcathianSurpriseSound",
            BuffTextureName = "KogMaw_IcathianSurprise.dds",
        };
    }
}