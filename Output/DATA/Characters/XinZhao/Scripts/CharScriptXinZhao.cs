#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptXinZhao : BBCharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_BleedAmount; // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            nextBuffVars_BleedAmount = 0.4f;
            AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoPuncture(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.ComboCounter = 0;
        }
        public override void OnResurrect()
        {
            AddBuff((ObjAIBase)owner, target, new Buffs.XenZhaoPuncture(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoBattleCryPassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 1)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.XenZhaoBattleCryPH)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoBattleCryPassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}