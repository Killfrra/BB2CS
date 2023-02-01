#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _352 : BBCharScript
    {
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float dRPERC;
            float damageMultiplier;
            dRPERC = talentLevel * 0.005f;
            damageMultiplier = 1 - dRPERC;
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= damageMultiplier;
                if(damageAmount < 0)
                {
                    damageAmount = 0;
                }
            }
        }
    }
}