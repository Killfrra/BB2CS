#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_Aegis_Counter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pantheon Aegis Counter",
            BuffTextureName = "Pantheon_AOZ_Charging.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}