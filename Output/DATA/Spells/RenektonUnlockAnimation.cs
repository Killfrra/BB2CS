#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonUnlockAnimation : BBBuffScript
    {
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateActions()
        {
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}