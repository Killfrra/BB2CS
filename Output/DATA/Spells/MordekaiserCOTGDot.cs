#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserCOTGDot : BBBuffScript
    {
        float damageToDeal;
        bool doOnce;
        float[] effect0 = {0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f};
        public MordekaiserCOTGDot(float damageToDeal = default)
        {
            this.damageToDeal = damageToDeal;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageToDeal);
            this.doOnce = false;
            ApplyDamage((ObjAIBase)owner, attacker, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            this.doOnce = true;
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            int level;
            float percentLeech;
            float shieldAmount;
            if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                if(!this.doOnce)
                {
                    level = GetLevel(owner);
                    percentLeech = this.effect0[level];
                    shieldAmount = percentLeech * damageAmount;
                    IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                    IncHealth(owner, damageAmount, owner);
                    this.doOnce = true;
                }
            }
        }
    }
}