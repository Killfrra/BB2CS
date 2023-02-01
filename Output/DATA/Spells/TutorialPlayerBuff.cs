#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TutorialPlayerBuff : BBBuffScript
    {
        public override void OnActivate()
        {
            IncPermanentFlatPhysicalDamageMod(owner, 20);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth;
            float curHealthPercent;
            float _1; // UNITIALIZED
            float damageMod;
            curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            curHealthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            damageMod = curHealthPercent + _1;
            damageAmount *= damageMod;
            if(damageAmount >= curHealth)
            {
                damageAmount = curHealth - 1;
            }
        }
    }
}