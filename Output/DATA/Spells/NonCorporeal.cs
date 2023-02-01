#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NonCorporeal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Non-Corporeal",
            BuffTextureName = "Voidwalker_DampingVoid.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, 30);
        }
    }
}