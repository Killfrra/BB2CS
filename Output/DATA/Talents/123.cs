#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _123 : BBCharScript
    {
        float[] effect0 = {1.04f, 1.08f};
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                float havocDamage;
                level = talentLevel;
                havocDamage = this.effect0[level];
                damageAmount *= havocDamage;
            }
        }
    }
}