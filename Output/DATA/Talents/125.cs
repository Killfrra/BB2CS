#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _125 : BBCharScript
    {
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= 0.96f;
                if(damageAmount < 0)
                {
                    damageAmount = 0;
                }
            }
        }
    }
}