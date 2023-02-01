#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptRabidWolf : BBCharScript
    {
        public override void OnUpdateActions()
        {
            float nextBuffVars_HPPerLevel;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CrestOfNaturesFury)) > 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CrestOfNaturesFury(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 100000);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) > 0)
            {
            }
            else
            {
                nextBuffVars_HPPerLevel = 130;
                AddBuff((ObjAIBase)owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            damageAmount *= 1.43f;
        }
    }
}