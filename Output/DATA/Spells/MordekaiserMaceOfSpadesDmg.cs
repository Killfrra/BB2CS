#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserMaceOfSpadesDmg : BBBuffScript
    {
        float baseDamage;
        int count;
        int count; // UNUSED
        float[] effect0 = {0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f};
        float[] effect1 = {0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f};
        public MordekaiserMaceOfSpadesDmg(float baseDamage = default)
        {
            this.baseDamage = baseDamage;
        }
        public override void OnActivate()
        {
            this.count = 0;
            if(attacker != owner)
            {
                //RequireVar(this.baseDamage);
                ApplyDamage((ObjAIBase)owner, attacker, this.baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, (ObjAIBase)owner);
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
            if(target is Champion)
            {
                percentLeech = this.effect0[level];
            }
            else
            {
                percentLeech = this.effect1[level];
            }
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
                else if(target is BaseTurret)
                {
                    shieldAmount = 0.3f + percentLeech * damageAmount;
                    IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                    this.count = 1;
                }
            }
        }
    }
}