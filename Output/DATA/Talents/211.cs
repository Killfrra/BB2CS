#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _211 : BBCharScript
    {
        public override void OnUpdateActions()
        {
            string dotCheck;
            float cooldown;
            string dotCheck2;
            dotCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(dotCheck == nameof(Spells.SummonerDot))
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if(cooldown > 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BurningEmbers)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.BurningEmbers(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                else
                {
                    SpellBuffRemove(owner, nameof(Buffs.BurningEmbers), (ObjAIBase)owner, 0);
                }
            }
            dotCheck2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(dotCheck2 == nameof(Spells.SummonerDot))
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if(cooldown > 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BurningEmbers)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.BurningEmbers(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                else
                {
                    SpellBuffRemove(owner, nameof(Buffs.BurningEmbers), (ObjAIBase)owner, 0);
                }
            }
        }
        public override void SetVarsByLevel()
        {
            avatarVars.OffensiveMastery = talentLevel;
        }
    }
}