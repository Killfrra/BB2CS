#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class YorickReviveAlly : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool zombie;
            AddBuff((ObjAIBase)owner, target, new Buffs.YorickReviveAllySelf(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            zombie = GetIsZombie(owner);
            if(!zombie)
            {
                SpellCast((ObjAIBase)owner, target, owner.Position, owner.Position, 3, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
        }
    }
}