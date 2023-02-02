#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class XerathArcanopulseExtended : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamID;
            Vector3 ownerPos;
            float distance; // UNUSED
            Vector3 beam1;
            Vector3 beam3;
            Minion other1;
            Minion other3;
            targetPos = GetCastSpellTargetPos();
            teamID = GetTeamID(owner);
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            beam1 = GetPointByUnitFacingOffset(owner, 145, 0);
            beam3 = GetPointByUnitFacingOffset(owner, 1600, 0);
            other1 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam1, teamID ?? TeamId.TEAM_NEUTRAL, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            other3 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam3, teamID ?? TeamId.TEAM_NEUTRAL, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            FaceDirection(other1, other3.Position);
            LinkVisibility(other1, other3);
            AddBuff(attacker, other1, new Buffs.XerathArcanopulseDeath(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.XerathArcanopulseDeath(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other3, other1, new Buffs.XerathArcanopulseWPartFix(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other3, other1, new Buffs.XerathArcanopulseWPartFix2(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other1, other3, new Buffs.XerathArcanopulseBeam(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other1, new Buffs.XerathArcanopulseBall(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}