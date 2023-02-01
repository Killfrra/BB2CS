#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DoubleStrikeIcon : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "DoubleStrikeReady",
            BuffTextureName = "MasterYi_DoubleStrike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}