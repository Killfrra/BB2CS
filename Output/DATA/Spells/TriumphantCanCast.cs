#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TriumphantCanCast : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Buff_TriumphantCanCast",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        public override void OnActivate()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true);
        }
    }
}