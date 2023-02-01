#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Kindlegem : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spirit Visage",
            BuffTextureName = "3065_Spirit_Visage.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.1f);
        }
    }
}