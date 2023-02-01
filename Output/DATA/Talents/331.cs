#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _331 : BBCharScript
    {
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float flatDR;
            flatDR = talentLevel * 1;
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount -= flatDR;
                if(damageAmount < 0)
                {
                    damageAmount = 0;
                }
            }
        }
    }
}