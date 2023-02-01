#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliTwinDmg : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "L_hand", "", "", },
            AutoBuffActivateEffect = new[]{ "akali_twinDisciplines_DMG_buf.troy", "", "", },
            BuffName = "AkaliTwinDmg",
            BuffTextureName = "AkaliTwinDisciplines.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float akaliDmg;
        float baseVampPercent;
        float additionalVampPercent;
        float vampPercentTooltip;
        float lastTimeExecuted;
        public AkaliTwinDmg(float akaliDmg = default)
        {
            this.akaliDmg = akaliDmg;
        }
        public override void OnActivate()
        {
            //RequireVar(this.akaliDmg);
            this.baseVampPercent = 0.08f;
            this.akaliDmg -= 10;
            this.additionalVampPercent = this.akaliDmg / 600;
            charVars.VampPercent = this.baseVampPercent + this.additionalVampPercent;
            this.vampPercentTooltip = 100 * charVars.VampPercent;
            SetBuffToolTipVar(1, this.vampPercentTooltip);
        }
        public override void OnUpdateStats()
        {
            IncPercentSpellVampMod(owner, charVars.VampPercent);
        }
        public override void OnUpdateActions()
        {
            this.akaliDmg = GetFlatPhysicalDamageMod(owner);
            this.akaliDmg -= 10;
            this.additionalVampPercent = this.akaliDmg / 600;
            charVars.VampPercent = this.baseVampPercent + this.additionalVampPercent;
            if(ExecutePeriodically(2, ref this.lastTimeExecuted, false))
            {
                this.vampPercentTooltip = 100 * charVars.VampPercent;
                SetBuffToolTipVar(1, this.vampPercentTooltip);
            }
        }
    }
}