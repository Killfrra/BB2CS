#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenTriCleaveSoundTwo : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RivenTriCleaveBuff",
            BuffTextureName = "RivenBrokenWings.dds",
            SpellToggleSlot = 1,
        };
    }
}