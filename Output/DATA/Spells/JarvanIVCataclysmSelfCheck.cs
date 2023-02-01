#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVCataclysmSelfCheck : BBBuffScript
    {
        float newCd;
        int[] effect0 = {-100, -125, -150};
        int[] effect1 = {120, 105, 90, 0, 0};
        public override void OnActivate()
        {
            int level;
            float manaReduction;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            manaReduction = this.effect0[level];
            this.newCd = this.effect1[level];
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, manaReduction, PrimaryAbilityResourceType.MANA);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.newCd;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Target, owner);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SpellBuffClear(owner, nameof(Buffs.JarvanIVCataclysm));
        }
    }
}