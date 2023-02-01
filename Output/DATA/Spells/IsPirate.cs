#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IsPirate : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Is Pirate",
            BuffTextureName = "IsPirate.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
    }
}