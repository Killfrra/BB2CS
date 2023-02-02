#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DesperatePower_marker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Desperate Power",
            BuffTextureName = "Ryze_DesperatePower.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = {40, 40, 40, 40, 40, 40, 80, 80, 80, 80, 80, 80, 120, 120, 120, 120, 120, 120};
        public override void OnActivate()
        {
            this.lastTooltip = 0;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                int level;
                float tooltipAmount;
                level = GetLevel(owner);
                tooltipAmount = this.effect0[level];
                if(tooltipAmount > this.lastTooltip)
                {
                    this.lastTooltip = tooltipAmount;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DesperatePower)) == 0)
            {
                float healthPercent;
                healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if(healthPercent <= 0.4f)
                {
                    object nextBuffVars_AddSpellDamage; // UNUSED
                    nextBuffVars_AddSpellDamage = charVars.AddSpellDamage;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.DesperatePower(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0.75f);
                }
            }
        }
    }
}