#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Focus : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Focus",
            BuffTextureName = "Bowmaster_Bullseye.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        float lastCrit;
        float[] effect0 = {0.03f, 0.03f, 0.03f, 0.06f, 0.06f, 0.06f, 0.09f, 0.09f, 0.09f, 0.12f, 0.12f, 0.12f, 0.15f, 0.15f, 0.15f, 0.18f, 0.18f, 0.18f};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                int level;
                float currentCrit;
                level = GetLevel(owner);
                currentCrit = this.effect0[level];
                if(currentCrit > this.lastCrit)
                {
                    float tooltipCritChance;
                    tooltipCritChance = 100 * currentCrit;
                    this.lastCrit = currentCrit;
                    SetBuffToolTipVar(1, tooltipCritChance);
                }
            }
        }
        public override void OnActivate()
        {
            this.lastCrit = 0;
        }
    }
}