#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CaitlynHeadshotpassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Headshot Marker",
            BuffTextureName = "Caitlyn_Headshot.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = {7, 7, 7, 7, 7, 7, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5};
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 8);
            this.lastTooltip = 8;
        }
        public override void OnUpdateActions()
        {
            int level;
            float buffTooltip;
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                level = GetLevel(owner);
                charVars.TooltipAmount = this.effect0[level];
                if(charVars.TooltipAmount < this.lastTooltip)
                {
                    charVars.LastTooltip = charVars.TooltipAmount;
                    buffTooltip = charVars.TooltipAmount + 1;
                    SetBuffToolTipVar(1, buffTooltip);
                }
            }
        }
    }
}