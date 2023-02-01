#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_CertainDeath : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PantheonCertainDeath",
            BuffTextureName = "Pantheon_CD.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}