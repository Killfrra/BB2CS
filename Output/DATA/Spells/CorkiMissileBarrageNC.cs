#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CorkiMissileBarrageNC : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CorkiMissileBarrageCounterNormal",
            BuffTextureName = "CorkiMissileBarrageNormal.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}