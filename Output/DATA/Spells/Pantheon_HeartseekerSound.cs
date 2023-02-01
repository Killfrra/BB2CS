#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_HeartseekerSound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pantheon HeartseekerSound",
            BuffTextureName = "Pantheon_HSS.dds",
        };
    }
}