#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PirateScurvy : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PirateScurvy",
            BuffTextureName = "Pirate_RemoveScurvy.dds",
            NonDispellable = true,
        };
    }
}