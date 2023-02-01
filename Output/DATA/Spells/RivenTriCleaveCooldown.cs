#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenTriCleaveCooldown : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RivenTriCleaveBuff",
            BuffTextureName = "Riven_Buffer.dds",
            SpellToggleSlot = 1,
        };
        public override void OnDeactivate(bool expired)
        {
            float duration;
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RivenTriCleave));
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.RivenTriCleaveCooldown));
            duration /= 1;
            SetSlotSpellCooldownTimeVer2(duration, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}