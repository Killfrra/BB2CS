#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JackInTheBoxDamageSensor : BBBuffScript
    {
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(target is Champion)
            {
                if(damageSource == default)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.JackInTheBoxHardLock(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if(damageSource == default)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.JackInTheBoxHardLock(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            SetTriggerUnit(attacker);
            if(attacker is Champion)
            {
                if(owner.Team != attacker.Team)
                {
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.JackInTheBoxHardLock(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}