#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LuxMaliceCannon : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
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
            Vector3 damagePoint;
            targetPos = GetCastSpellTargetPos();
            teamID = GetTeamID(owner);
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            beam1 = GetPointByUnitFacingOffset(owner, 145, 0);
            beam3 = GetPointByUnitFacingOffset(owner, 3300, 0);
            other1 = SpawnMinion("hiu", "TestCubeRender", "idle.lua", beam1, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 450, default, false, (Champion)owner);
            other3 = SpawnMinion("hiu", "TestCube", "idle.lua", beam3, teamID ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 450, default, false, (Champion)owner);
            LinkVisibility(other1, other3);
            AddBuff(attacker, other1, new Buffs.LuxMaliceCannonDeathFix(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, other3, new Buffs.LuxMaliceCannonDeathFix(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(other3, other3, new Buffs.ExpirationTimer(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(other1, owner, new Buffs.LuxMaliceCannonPartFix(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(other1, owner, new Buffs.LuxMaliceCannonPartFix2(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(other1, other3, new Buffs.LuxMaliceCannonBeam(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(other1, other1, new Buffs.LuxMaliceCannonBall(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            damagePoint = GetPointByUnitFacingOffset(owner, 1650, 0);
            SpellEffectCreate(out _, out _, "LuxMaliceCannon_beammiddle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, damagePoint, default, default, default, false);
        }
    }
}