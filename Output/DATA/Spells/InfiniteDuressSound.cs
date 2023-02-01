#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class InfiniteDuressSound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "InfiniteDuressSound",
            BuffTextureName = "Wolfman_InfiniteDuress.dds",
            SpellVOOverrideSkins = new[]{ "HyenaWarwick", },
        };
    }
}