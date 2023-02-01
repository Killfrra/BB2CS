#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Tailwind : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Tailwind",
            BuffTextureName = "Janna_Tailwind.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.03f);
        }
    }
}