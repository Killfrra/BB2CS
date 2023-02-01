#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SpeedBoost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SpeedBoost",
        };
        float speedBoost;
        public SpeedBoost(float speedBoost = default)
        {
            this.speedBoost = speedBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedBoost);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.speedBoost);
        }
    }
}