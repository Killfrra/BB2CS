#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShellBash : BBSpellScript
    {
        int[] effect0 = {100, 125, 150, 175, 200};
        float[] effect1 = {1, 1.5f, 2, 2.5f, 3};
        public override bool CanCast()
        {
            bool returnValue = true;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DefensiveBallCurl)) > 0)
            {
                returnValue = false;
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PowerBall)) > 0)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1);
            ApplyStun(attacker, target, this.effect1[level]);
        }
    }
}