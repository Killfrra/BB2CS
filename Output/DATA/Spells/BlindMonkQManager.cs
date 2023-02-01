#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkQManager : BBBuffScript
    {
        int[] effect0 = {7, 6, 5, 4, 3};
        public override void OnActivate()
        {
            float cooldown; // UNUSED
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlindMonkQTwo));
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.BlindMonkQOne)) == 0)
            {
                if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.BlindMonkQOneChaos)) == 0)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float cD;
            float duration;
            float preCDRCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cD = this.effect0[level];
            duration = 3 - lifeTime;
            duration = Math.Max(0, duration);
            preCDRCooldown = cD + duration;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * preCDRCooldown;
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlindMonkQOne));
            SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}