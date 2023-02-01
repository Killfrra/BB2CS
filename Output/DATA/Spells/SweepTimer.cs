#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SweepTimer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            bool unitFound;
            unitFound = false;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 200, SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 1))
            {
                unitFound = true;
                SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 0, SpellSlotType.SpellSlots, 1, false, false, false);
            }
            if(!unitFound)
            {
                SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 2, SpellSlotType.SpellSlots, 1, false, false, false);
            }
        }
    }
}