#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _234 : BBCharScript
    {
        float[] effect0 = {1.005f, 1.01f, 1.015f, 1.02f};
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float havocDamage;
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                level = talentLevel;
                havocDamage = this.effect0[level];
                damageAmount *= havocDamage;
            }
        }
    }
}