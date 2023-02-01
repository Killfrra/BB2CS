#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickRAPetDmg : BBBuffScript
    {
        int count;
        float damageToDeal;
        int count; // UNUSED
        float[] effect0 = {0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.275f, 0.275f, 0.275f, 0.275f, 0.275f, 0.275f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f};
        public YorickRAPetDmg(float damageToDeal = default)
        {
            this.damageToDeal = damageToDeal;
        }
        public override void OnActivate()
        {
            this.count = 0;
            if(attacker != owner)
            {
                //RequireVar(this.damageToDeal);
                ApplyDamage((ObjAIBase)owner, attacker, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            ObjAIBase caster;
            int level;
            float percentLeech;
            float shieldAmount;
            caster = SetBuffCasterUnit();
            level = GetLevel(owner);
            percentLeech = this.effect0[level];
            if(caster != owner)
            {
                if(this.count == 0)
                {
                    shieldAmount = percentLeech * damageAmount;
                    IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                    this.count = 1;
                }
            }
            else
            {
                if(target is not ObjAIBase)
                {
                    shieldAmount = percentLeech * damageAmount;
                    IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                    this.count = 1;
                }
            }
        }
    }
}