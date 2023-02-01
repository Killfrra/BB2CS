#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WallofPainTarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", },
            BuffName = "Wall of Pain Slow",
            BuffTextureName = "Lich_WallOfPain.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        float numTicks;
        float lastTimeExecuted;
        public WallofPainTarget(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
            this.numTicks = 20;
        }
        public override void OnUpdateStats()
        {
            float curSlowPercent;
            float speedMod;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                this.numTicks--;
            }
            curSlowPercent = this.numTicks / 20;
            speedMod = curSlowPercent * this.moveSpeedMod;
            IncPercentMultiplicativeMovementSpeedMod(owner, speedMod);
        }
    }
}