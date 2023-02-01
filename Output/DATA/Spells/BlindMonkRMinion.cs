#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkRMinion : BBBuffScript
    {
        Vector3 targetPos;
        public BlindMonkRMinion(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            int level;
            Vector3 ownerPos;
            Vector3 targetPos;
            //RequireVar(this.targetPos);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ownerPos = GetUnitPosition(owner);
            targetPos = this.targetPos;
            SpellCast((ObjAIBase)owner, attacker, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, true, ownerPos);
        }
    }
}