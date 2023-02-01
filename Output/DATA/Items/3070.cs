#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3070 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.95f, ref this.lastTimeExecuted, false))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.TearOfTheGoddessTrack(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TearOfTheGoddessTrack)) > 0)
            {
            }
            else
            {
                charVars.TearBonusMana = 0;
            }
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
    }
}