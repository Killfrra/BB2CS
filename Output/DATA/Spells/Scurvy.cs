#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Scurvy : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Scurvy",
            BuffTextureName = "Pirate_GrogSoakedBlade.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21};
        int[] effect1 = {4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21};
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
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            int nextBuffVars_DotDamage;
            float nextBuffVars_moveSpeedMod;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    level = GetLevel(owner);
                    nextBuffVars_DotDamage = this.effect1[level];
                    nextBuffVars_moveSpeedMod = -0.07f;
                    AddBuff((ObjAIBase)owner, target, new Buffs.ScurvyStrikeParticle(), 3, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.SLOW, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.ScurvyStrike(nextBuffVars_DotDamage, nextBuffVars_moveSpeedMod), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}