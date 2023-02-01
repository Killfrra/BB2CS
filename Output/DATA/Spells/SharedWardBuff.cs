#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SharedWardBuff : BBBuffScript
    {
        public override void OnActivate()
        {
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, nameof(Buffs.ResistantSkin), true))
            {
                MoveAway(owner, unit.Position, 1000, 50, 300, 300, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 300, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            }
        }
    }
}