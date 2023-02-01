#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IsPirateHunter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "IsPirateHunter",
            BuffTextureName = "IsPirate.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
    }
}