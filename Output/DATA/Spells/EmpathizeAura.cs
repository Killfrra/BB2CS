#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EmpathizeAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Empathize_buf.troy", },
            BuffName = "Empathize",
            BuffTextureName = "FallenAngel_Empathize.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float vampPercent;
        float[] effect0 = {0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f};
        public override void OnActivate()
        {
            float tooltipAmount;
            this.vampPercent = 0.1f;
            tooltipAmount = 100 * this.vampPercent;
            SetBuffToolTipVar(1, tooltipAmount);
        }
        public override void OnUpdateStats()
        {
            IncPercentSpellVampMod(owner, this.vampPercent);
        }
        public override void OnLevelUp()
        {
            int level;
            float newVampPercent;
            float tooltipAmount;
            level = GetLevel(owner);
            newVampPercent = this.effect0[level];
            if(newVampPercent != this.vampPercent)
            {
                this.vampPercent = newVampPercent;
                tooltipAmount = 100 * this.vampPercent;
                SetBuffToolTipVar(1, tooltipAmount);
            }
        }
    }
}