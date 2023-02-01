#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTwistedLizardElder : BBCharScript
    {
        public override void OnUpdateActions()
        {
            float nextBuffVars_HPPerLevel;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BlessingoftheLizardElder)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BlessingoftheLizardElder(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 100000, true, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) == 0)
            {
                nextBuffVars_HPPerLevel = 175;
                AddBuff((ObjAIBase)owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            damageAmount *= 1.43f;
        }
    }
}