#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptRyze : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                float mana;
                float bonusDamage;
                float bonusDamage2;
                mana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
                bonusDamage = mana * 0.08f;
                bonusDamage2 = mana * 0.05f;
                SetSpellToolTipVar(bonusDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                SetSpellToolTipVar(bonusDamage2, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ArcaneMastery(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Overload(), 1, 1, 20000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(slot == 3)
            {
                IncPermanentFlatPARPoolMod(owner, 75, PrimaryAbilityResourceType.MANA);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}