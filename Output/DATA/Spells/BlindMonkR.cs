#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BlindMonkR : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 4, 4, 4, 4, 4, },
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            returnValue = false;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.BlindMonkRMarker), true))
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamID;
            Minion other2;
            Vector3 teleportPos;
            Vector3 ownerPos;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.BlindMonkRMarker), true))
            {
                targetPos = GetCastSpellTargetPos();
                teamID = GetTeamID(owner);
                other2 = SpawnMinion("TestMinion", "TestCubeRender", "idle.lua", targetPos, teamID, false, true, false, false, false, true, 0, default, true);
                AddBuff(other2, other2, new Buffs.BlindMonkRNewMinion(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                FaceDirection(unit, targetPos);
                teleportPos = GetPointByUnitFacingOffset(unit, 100, 180);
                TeleportToPosition(owner, teleportPos);
                ownerPos = GetUnitPosition(owner);
                SpellCast((ObjAIBase)owner, unit, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, true, ownerPos);
            }
        }
    }
}