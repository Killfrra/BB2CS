#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GragasExplosiveCask : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
        };
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            Minion other2;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(targetPos, ownerPos);
            FaceDirection(owner, targetPos);
            if(distance > 1050)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1050, 0);
            }
            other2 = SpawnMinion("k", "TestCubeRender", "idle.lua", targetPos, teamID ?? TeamId.TEAM_NEUTRAL, true, true, false, true, true, true, 0, default, true, (Champion)attacker);
            SpellCast((ObjAIBase)owner, other2, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}