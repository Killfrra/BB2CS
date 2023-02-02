#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ActionTimer2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            bool _false; // UNITIALIZED
            bool foundUnit;
            foundUnit = _false;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.Stealth), false))
            {
                bool canSee;
                foundUnit = _false;
                canSee = CanSeeTarget(owner, unit);
                if(canSee)
                {
                    FaceDirection(owner, unit.Position);
                    SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 3, SpellSlotType.SpellSlots, 1, false, false, false, false, false, false);
                }
                else
                {
                    AddBuff(attacker, target, new Buffs.ActionTimer(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            if(!foundUnit)
            {
                AddBuff(attacker, target, new Buffs.ActionTimer(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
    }
}