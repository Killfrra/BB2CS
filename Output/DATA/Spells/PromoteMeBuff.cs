#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PromoteMeBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PromoteMeBuff",
            BuffTextureName = "",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}