#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3004 : BBItemScript
    {
        float maxMana; // UNUSED
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            this.maxMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            if(ExecutePeriodically(0.9f, ref this.lastTimeExecuted, false))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.TearOfTheGoddessTrack(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.ManamuneAttackTrack(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.ManamuneAttackConversion(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
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
namespace Buffs
{
    public class _3004 : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            int slot; // UNITIALIZED
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
    }
}