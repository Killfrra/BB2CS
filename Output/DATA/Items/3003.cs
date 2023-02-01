#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3003 : BBItemScript
    {
        float maxMana;
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float bonusAbilityPower;
            bonusAbilityPower = 0.03f * this.maxMana;
            IncFlatMagicDamageMod(owner, bonusAbilityPower);
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnUpdateActions()
        {
            this.maxMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            if(ExecutePeriodically(0.9f, ref this.lastTimeExecuted, false))
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