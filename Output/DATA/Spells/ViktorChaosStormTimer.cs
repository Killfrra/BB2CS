#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorChaosStormTimer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "ViktorStormTimer",
            BuffTextureName = "ViktorChaosStormGuide.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}