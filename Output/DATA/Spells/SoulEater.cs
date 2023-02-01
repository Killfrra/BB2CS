#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SoulEater : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SoulEater",
            BuffTextureName = "Nasus_SoulEater.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastLifesteal;
        float lastTimeExecuted;
        int[] effect0 = {14, 14, 14, 14, 14, 17, 17, 17, 17, 17, 20, 20, 20, 20, 20, 20, 20, 20};
        public override void OnActivate()
        {
            this.lastLifesteal = 0;
        }
        public override void OnUpdateActions()
        {
            int level;
            float currentLifesteal;
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                level = GetLevel(owner);
                currentLifesteal = this.effect0[level];
                if(currentLifesteal > this.lastLifesteal)
                {
                    this.lastLifesteal = currentLifesteal;
                    SetBuffToolTipVar(1, currentLifesteal);
                }
            }
        }
    }
}