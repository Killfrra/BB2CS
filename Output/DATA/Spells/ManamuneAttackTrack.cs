#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ManamuneAttackTrack : BBBuffScript
    {
        float cooldownResevoir;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.cooldownResevoir = 0;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(3, ref this.lastTimeExecuted, true))
            {
                if(this.cooldownResevoir < 2)
                {
                    this.cooldownResevoir++;
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            bool spellVars_DoesntTriggerSpellCasts; // UNITIALIZED
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                if(this.cooldownResevoir > 0)
                {
                    Particle killMe_; // UNUSED
                    SpellEffectCreate(out killMe_, out _, "TearoftheGoddess_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                    charVars.TearBonusMana++;
                    charVars.TearBonusMana = Math.Min(charVars.TearBonusMana, 1000);
                    this.cooldownResevoir += -1;
                }
            }
        }
    }
}