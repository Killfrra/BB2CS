#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliTwinAP : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "R_hand", null, "", },
            AutoBuffActivateEffect = new[]{ "", "akali_twinDisciplines_AP_buf.troy", },
            BuffName = "AkaliTwinAP",
            BuffTextureName = "AkaliTwinDisciplines.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float akaliAP;
        float bonusDmgPerc;
        float addBonusDmgPerc;
        float dmgMult;
        float lastTimeExecuted;
        public AkaliTwinAP(float akaliAP = default)
        {
            this.akaliAP = akaliAP;
        }
        public override void OnActivate()
        {
            float dmgMultTooltip;
            //RequireVar(this.akaliAP);
            this.bonusDmgPerc = 0.08f;
            this.akaliAP -= 20;
            this.addBonusDmgPerc = this.akaliAP / 600;
            this.dmgMult = this.bonusDmgPerc + this.addBonusDmgPerc;
            dmgMultTooltip = 100 * this.dmgMult;
            SetBuffToolTipVar(1, dmgMultTooltip);
        }
        public override void OnUpdateActions()
        {
            float dmgMultTooltip;
            this.akaliAP = GetFlatMagicDamageMod(owner);
            this.akaliAP -= 20;
            this.addBonusDmgPerc = this.akaliAP / 600;
            this.dmgMult = this.bonusDmgPerc + this.addBonusDmgPerc;
            if(ExecutePeriodically(2, ref this.lastTimeExecuted, false))
            {
                dmgMultTooltip = 100 * this.dmgMult;
                SetBuffToolTipVar(1, dmgMultTooltip);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float tAD;
            float damageToDeal;
            tAD = GetTotalAttackDamage(attacker);
            damageToDeal = tAD * this.dmgMult;
            if(target is not BaseTurret)
            {
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            }
        }
    }
}