#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GragasBodySlamSelfSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Slow.troy", },
            BuffTextureName = "Nidalee_Pounce.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, 0);
        }
    }
}