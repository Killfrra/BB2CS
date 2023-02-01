#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3200 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(9, ref this.lastTimeExecuted, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.ViktorHexCore(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SetSpellToolTipVar(charVars.BonusForItem, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
    }
}