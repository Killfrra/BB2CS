#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GlobalDrainMana : BBBuffScript
    {
        float drainPercent;
        float manaDrainPercent;
        bool drainedBool;
        public GlobalDrainMana(float drainPercent = default, float manaDrainPercent = default)
        {
            this.drainPercent = drainPercent;
            this.manaDrainPercent = manaDrainPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.drainPercent);
            //RequireVar(this.manaDrainPercent);
            this.drainedBool = false;
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
            if(damageSource == default)
            {
                if(!this.drainedBool)
                {
                    float drainHealth;
                    float drainMana;
                    drainHealth = damageAmount * this.drainPercent;
                    IncHealth(attacker, drainHealth, attacker);
                    drainMana = damageAmount * this.manaDrainPercent;
                    IncPAR(owner, drainMana, PrimaryAbilityResourceType.MANA);
                    this.drainedBool = true;
                }
            }
        }
    }
}