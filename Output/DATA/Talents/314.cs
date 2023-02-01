#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _314 : BBCharScript
    {
        float smallDamageAmount;
        int[] effect0 = {1, 2, 3};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            this.smallDamageAmount = this.effect0[level];
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(attacker is ObjAIBase)
            {
                if(attacker is BaseTurret)
                {
                }
                else
                {
                    if(attacker is Champion)
                    {
                    }
                    else
                    {
                        damageAmount -= this.smallDamageAmount;
                    }
                }
            }
            if(damageAmount < 0)
            {
                damageAmount = 0;
            }
        }
    }
}