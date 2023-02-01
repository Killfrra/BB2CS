#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Paranoia : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Paranoia Aura",
            BuffTextureName = "Fiddlesticks_Paranoia.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}