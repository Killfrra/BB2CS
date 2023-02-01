#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenCounterReady : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "RenekthonSliceAndDiceDelay",
            BuffTextureName = "Renekton_Dice.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        public override void OnActivate()
        {
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RivenCounterDash));
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.RivenCounterReady));
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RivenCounter));
            SetSlotSpellCooldownTimeVer2(10, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}