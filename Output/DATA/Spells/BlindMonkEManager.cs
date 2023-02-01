#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkEManager : BBBuffScript
    {
        int[] effect0 = {7, 7, 7, 7, 7};
        public override void OnActivate()
        {
            float cooldown; // UNUSED
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlindMonkETwo));
            SetSlotSpellCooldownTimeVer2(0.1f, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
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
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cD = this.effect0[level];
            duration = 3 - lifeTime;
            duration = Math.Max(0, duration);
            preCDRCooldown = cD + duration;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * preCDRCooldown;
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlindMonkEOne));
            SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}