#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WrathTimer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 2, SpellSlotType.SpellSlots, 1, false, false, false);
        }
    }
}