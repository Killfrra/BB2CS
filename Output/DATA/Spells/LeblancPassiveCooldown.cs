#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeblancPassiveCooldown : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancPassiveCooldown",
            BuffTextureName = "LeblancMirrorImage_Charging.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}