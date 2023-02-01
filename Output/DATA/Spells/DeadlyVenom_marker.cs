#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeadlyVenom_marker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Deadly Venom Marker",
            BuffTextureName = "Twitch_DeadlyVenom_temp.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        float[] effect0 = {2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 5, 5, 5, 5, 5, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 10, 10, 10};
        public override void OnActivate()
        {
            this.lastTooltip = 0;
        }
        public override void OnUpdateActions()
        {
            int level;
            float tooltipAmount;
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                level = GetLevel(owner);
                tooltipAmount = this.effect0[level];
                if(tooltipAmount > this.lastTooltip)
                {
                    this.lastTooltip = tooltipAmount;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }
    }
}