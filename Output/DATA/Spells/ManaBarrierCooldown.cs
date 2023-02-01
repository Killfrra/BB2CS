#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ManaBarrierCooldown : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BlitzcrankManaBarrierCD",
            BuffTextureName = "Blitzcrank_ManaBarrierCD.dds",
            NonDispellable = true,
        };
    }
}