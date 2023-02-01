#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SoulShacklesOwner : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SoulShackles",
            BuffTextureName = "FallenAngel_Purgatory.dds",
        };
    }
}