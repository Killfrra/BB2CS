#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearWDrain : BBBuffScript
    {
        float drainPercent;
        bool drainedBool;
        public VolibearWDrain(float drainPercent = default)
        {
            this.drainPercent = drainPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.drainPercent);
            //RequireVar(this.drainedBool);
        }
        public override void OnUpdateStats()
        {
            if(this.drainedBool)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(!this.drainedBool)
            {
                float drainHealth;
                drainHealth = damageAmount * this.drainPercent;
                IncHealth(attacker, drainHealth, attacker);
                this.drainedBool = true;
            }
        }
    }
}