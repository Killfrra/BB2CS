#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MaokaiSapling2 : BBSpellScript
    {
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Region asdf; // UNUSED
            Vector3 ownerPos;
            float distance; // UNUSED
            Minion other2;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            asdf = AddPosPerceptionBubble(teamID, 250, targetPos, 1, default, false);
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(targetPos, ownerPos);
            FaceDirection(owner, targetPos);
            other2 = SpawnMinion("k", "TestCubeRender10Vision", "idle.lua", targetPos, teamID ?? TeamId.TEAM_NEUTRAL, true, true, false, true, true, true, 1, default, true, (Champion)attacker);
            AddBuff(attacker, other2, new Buffs.MaokaiSapling2(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast((ObjAIBase)owner, other2, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, false, false, false, false, false, false);
            AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class MaokaiSapling2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "C_BUFFBONE_GLB_CHEST_LOC", },
            AutoBuffActivateEffect = new[]{ "maokai_sapling_activated_indicator.troy", },
        };
        public override void OnActivate()
        {
            IncFlatBubbleRadiusMod(owner, 690);
        }
        public override void OnUpdateStats()
        {
            IncFlatBubbleRadiusMod(owner, 690);
        }
    }
}