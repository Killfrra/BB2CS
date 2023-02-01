#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FortifyCheck : BBBuffScript
    {
        public override void OnUpdateActions()
        {
            string foritfyCheck;
            float cooldown;
            string foritfyCheck2;
            foritfyCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(foritfyCheck == nameof(Spells.SummonerFortify))
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if(cooldown <= 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FortifyBuff)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FortifyBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                    }
                }
            }
            foritfyCheck2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(foritfyCheck2 == nameof(Spells.SummonerFortify))
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if(cooldown <= 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FortifyBuff)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FortifyBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                    }
                }
            }
        }
    }
}