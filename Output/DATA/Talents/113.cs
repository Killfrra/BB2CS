#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _113 : BBCharScript
    {
        float damageBlock;
        float[] effect0 = {1, 1.5f, 2};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            this.damageBlock = this.effect0[level];
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
            {
                damageAmount -= this.damageBlock;
            }
            if(damageAmount < 0)
            {
                damageAmount = 0;
            }
        }
    }
}