#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SkarnerUnlockAnimation : BBBuffScript
    {
        public override void OnActivate()
        {
            if(false)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(false)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            UnlockAnimation(owner, true);
        }
        public override void OnUpdateActions()
        {
            if(false)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
    }
}