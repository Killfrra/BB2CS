#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PickACard_tracker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pick A Card",
            BuffTextureName = "CardMaster_FatesGambit.dds",
        };
    }
}