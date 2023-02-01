#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FallenOneTimer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true);
        }
    }
}