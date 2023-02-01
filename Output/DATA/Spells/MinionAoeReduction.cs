#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MinionAoeReduction : BBBuffScript
    {
        float damageMultiplier;
        public override void OnActivate()
        {
            float gameTime;
            float aoeReduction;
            gameTime = GetGameTime();
            aoeReduction = gameTime * 0.000111f;
            aoeReduction = Math.Min(aoeReduction, 0.2f);
            aoeReduction = Math.Max(aoeReduction, 0);
            this.damageMultiplier = 1 - aoeReduction;
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(attacker is Champion)
            {
                if(damageSource == default)
                {
                    damageAmount *= this.damageMultiplier;
                }
            }
        }
    }
}