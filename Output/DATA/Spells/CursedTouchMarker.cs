#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CursedTouchMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CursedTouchMarker",
            BuffTextureName = "SadMummy_CorpseExplosion.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = {15, 15, 15, 15, 15, 15, 25, 25, 25, 25, 25, 25, 35, 35, 35, 35, 35, 35};
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