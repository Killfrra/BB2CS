#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzPassiveDisplayIcon : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "FizzSeastoneTrident",
            BuffTextureName = "FizzSeastonePassive.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}