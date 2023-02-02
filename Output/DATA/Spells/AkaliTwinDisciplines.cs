#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliTwinDisciplines : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliTwinDisciplines",
            BuffTextureName = "33.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float akaliAP;
        float akaliDmg;
        float bonusDmgPerc;
        float addBonusDmgPerc;
        float dmgMult;
        float baseVampPercent;
        float additionalVampPercent;
        float vampPercentTooltip;
        public override void OnActivate()
        {
            this.akaliAP = GetFlatMagicDamageMod(owner);
            this.akaliDmg = GetFlatPhysicalDamageMod(owner);
        }
        public override void OnUpdateStats()
        {
            float nextBuffVars_AkaliAP;
            if(this.akaliAP >= 19.5f)
            {
                float dmgMultTooltip;
                nextBuffVars_AkaliAP = this.akaliAP;
                this.bonusDmgPerc = 0.08f;
                this.akaliAP -= 20;
                this.addBonusDmgPerc = this.akaliAP / 600;
                this.dmgMult = this.bonusDmgPerc + this.addBonusDmgPerc;
                dmgMultTooltip = 100 * this.dmgMult;
                SetBuffToolTipVar(1, dmgMultTooltip);
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliTwinAP(nextBuffVars_AkaliAP), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            else
            {
                SetBuffToolTipVar(1, 0);
            }
            if(this.akaliDmg >= 9.5f)
            {
                float nextBuffVars_AkaliDmg;
                nextBuffVars_AkaliDmg = this.akaliDmg;
                this.baseVampPercent = 0.08f;
                this.akaliDmg -= 10;
                this.additionalVampPercent = this.akaliDmg / 600;
                charVars.VampPercent = this.baseVampPercent + this.additionalVampPercent;
                this.vampPercentTooltip = 100 * charVars.VampPercent;
                SetBuffToolTipVar(2, this.vampPercentTooltip);
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliTwinDmg(nextBuffVars_AkaliDmg), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            else
            {
                SetBuffToolTipVar(2, 0);
            }
        }
        public override void OnUpdateActions()
        {
            this.akaliAP = GetFlatMagicDamageMod(owner);
            this.akaliDmg = GetFlatPhysicalDamageMod(owner);
        }
    }
}