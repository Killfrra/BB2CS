#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptKennen : BBCharScript
    {
        int[] effect0 = {8, 7, 6, 5, 4};
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.IsNinja(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.Count = 0;
        }
        public override void OnResurrect()
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                int nextBuffVars_CounterVar; // UNUSED
                nextBuffVars_CounterVar = this.effect0[level];
                charVars.Count = 0;
                AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeProc(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                AddBuff((ObjAIBase)owner, owner, new Buffs.KennenDoubleStrikeProc(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}