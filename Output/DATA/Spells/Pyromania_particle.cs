#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pyromania_particle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "StunReady.troy", },
            BuffName = "Energized",
            BuffTextureName = "Annie_Brilliance.dds",
        };
    }
}