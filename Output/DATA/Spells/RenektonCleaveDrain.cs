#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonCleaveDrain : BBBuffScript
    {
        float drainPercent;
        float maxDrain;
        float drainCount;
        float drainAmount;
        public RenektonCleaveDrain(float drainPercent = default, float maxDrain = default)
        {
            this.drainPercent = drainPercent;
            this.maxDrain = maxDrain;
        }
        public override void OnActivate()
        {
            //RequireVar(this.drainPercent);
            //RequireVar(this.maxDrain);
            this.drainCount = 0;
            this.drainAmount = 0;
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float drainHealth;
            float drainCandidate;
            if(damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
            {
                drainHealth = damageAmount * this.drainPercent;
                if(target is Champion)
                {
                    drainHealth *= 4;
                }
                drainCandidate = this.maxDrain - this.drainAmount;
                drainHealth = Math.Min(drainHealth, drainCandidate);
                drainHealth = Math.Max(drainHealth, 0);
                IncHealth(attacker, drainHealth, attacker);
                this.drainCount++;
                this.drainAmount += drainHealth;
            }
        }
    }
}