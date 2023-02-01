#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FleetofFoot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Fleet of Foot",
            BuffTextureName = "Sivir_Sprint.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        float[] effect0 = {0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f};
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
                    tooltipAmount *= 100;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }
        public override void OnUpdateStats()
        {
            bool temp;
            temp = IsMoving(owner);
            if(temp)
            {
                IncFlatDodgeMod(owner, charVars.DodgeChance);
            }
        }
    }
}