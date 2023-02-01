#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptPoppy : BBCharScript
    {
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyValiantFighter(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
            charVars.DamageCount = 0;
            charVars.ArmorCount = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level == 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyParagonManager(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}