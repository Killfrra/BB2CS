#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserNukeOfTheBeastDmg : BBBuffScript
    {
        float baseDamage;
        float[] effect0 = {0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f};
        float[] effect1 = {0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f};
        public MordekaiserNukeOfTheBeastDmg(float baseDamage = default)
        {
            this.baseDamage = baseDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.baseDamage);
            ApplyDamage((ObjAIBase)owner, attacker, this.baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            int level;
            float percentLeech;
            float shieldAmount;
            level = GetLevel(owner);
            if(target is Champion)
            {
                percentLeech = this.effect0[level];
            }
            else
            {
                percentLeech = this.effect1[level];
            }
            shieldAmount = percentLeech * damageAmount;
            IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
        }
    }
}