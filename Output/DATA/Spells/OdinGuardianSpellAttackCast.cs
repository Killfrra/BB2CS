#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OdinGuardianSpellAttackCast : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Stun)) > 0)
            {
            }
            else
            {
                SpellCast((ObjAIBase)owner, target, default, default, 0, SpellSlotType.ExtraSlots, 1, false, false, false, false, false, false);
            }
        }
    }
}