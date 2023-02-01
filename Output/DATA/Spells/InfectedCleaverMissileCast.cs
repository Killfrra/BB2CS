#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class InfectedCleaverMissileCast : BBSpellScript
    {
        int[] effect0 = {50, 60, 70, 80, 90};
        public override bool CanCast()
        {
            bool returnValue = true;
            int healthCost;
            float health;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            healthCost = this.effect0[level];
            health = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(health <= healthCost)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 1000)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 850, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}