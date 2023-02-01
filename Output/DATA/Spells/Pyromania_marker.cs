#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pyromania_marker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pyromania Marker",
            BuffTextureName = "Annie_Brilliance.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}