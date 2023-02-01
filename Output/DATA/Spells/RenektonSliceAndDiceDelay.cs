#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonSliceAndDiceDelay : BBBuffScript
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
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RenektonDice));
        }
        public override void OnDeactivate(bool expired)
        {
            float duration;
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.RenektonSliceAndDiceTimer));
            SpellBuffClear(owner, nameof(Buffs.RenektonSliceAndDiceTimer));
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RenektonSliceAndDice));
            SetSlotSpellCooldownTimeVer2(duration, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
        }
    }
}