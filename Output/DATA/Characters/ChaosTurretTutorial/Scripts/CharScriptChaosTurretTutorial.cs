#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptChaosTurretTutorial : BBCharScript
    {
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth;
            object dAMAGESOURCE_RAW; // UNITIALIZED
            curHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
            if(damageAmount >= curHealth)
            {
                if(attacker is Champion)
                {
                }
                else
                {
                    if(damageSource != default)
                    {
                        damageAmount = curHealth - 1;
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.TurretDamageManager(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 1);
        }
    }
}