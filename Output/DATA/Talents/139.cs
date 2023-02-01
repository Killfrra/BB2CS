#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _139 : BBCharScript
    {
        int[] effect0 = {15, 30};
        public override void SetVarsByLevel()
        {
            level = talentLevel;
            avatarVars.PromoteArmorBonus = 20;
            avatarVars.PromoteCooldownBonus = this.effect0[level];
        }
        public override void OnUpdateActions()
        {
            string foritfyCheck;
            float cooldown;
            string foritfyCheck2;
            foritfyCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(foritfyCheck == nameof(Spells.SummonerPromote))
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if(cooldown <= 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteBuff)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.PromoteBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                    }
                }
            }
            foritfyCheck2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(foritfyCheck2 == nameof(Spells.SummonerPromote))
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if(cooldown <= 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteBuff)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.PromoteBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                    }
                }
            }
        }
    }
}