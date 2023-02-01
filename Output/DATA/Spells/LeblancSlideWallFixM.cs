#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeblancSlideWallFixM : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancDisplacementM",
            BuffTextureName = "LeblancDisplacementReturnM.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 4,
        };
        int[] effect0 = {40, 32, 24};
        public override void OnDeactivate(bool expired)
        {
            int level;
            float baseCooldown;
            float cooldownPerc;
            UnlockAnimation(owner, true);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSlideM));
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseCooldown = this.effect0[level];
            cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= baseCooldown;
            SetSlotSpellCooldownTimeVer2(cooldownPerc, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}