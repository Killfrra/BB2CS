#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenTriCleaveBuffered : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        bool championLock;
        Vector3 targetPos;
        int level;
        public RivenTriCleaveBuffered(bool championLock = default, Vector3 targetPos = default, int level = default)
        {
            this.championLock = championLock;
            this.targetPos = targetPos;
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.level);
        }
        public override void OnDeactivate(bool expired)
        {
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, this.targetPos, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                this.targetPos = GetUnitPosition(unit);
            }
            if(this.championLock)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.RivenTriCleaveBufferLock), true))
                {
                    this.targetPos = GetUnitPosition(unit);
                }
            }
            SpellCast((ObjAIBase)owner, default, this.targetPos, this.targetPos, 4, SpellSlotType.ExtraSlots, this.level, true, false, false, false, false, false);
        }
    }
}