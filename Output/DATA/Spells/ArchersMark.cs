#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ArchersMark : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Archer's Mark",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        int[] effect0 = {1, 2, 3, 4, 5};
        public override void OnKill()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            IncGold(owner, this.effect0[level]);
        }
    }
}