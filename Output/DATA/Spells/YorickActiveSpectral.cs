#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickActiveSpectral : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "yorick_spectralGhoul_speed_buf.troy", },
            BuffName = "YorickSpectralBuffSelf",
            BuffTextureName = "YorickOmenOfWar.dds",
        };
        float movementSpeedPercent;
        public YorickActiveSpectral(float movementSpeedPercent = default)
        {
            this.movementSpeedPercent = movementSpeedPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedPercent);
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
            IncPercentMovementSpeedMod(owner, this.movementSpeedPercent);
        }
    }
}