#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RumbleCarpetBomb : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 targetPosStart;
            Vector3 targetPosEnd;
            Minion other1;
            TeamId teamID; // UNUSED
            teamOfOwner = GetTeamID(attacker);
            targetPosStart = GetCastSpellTargetPos();
            targetPosEnd = GetCastSpellDragEndPos();
            other1 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPosStart, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, default, true, (Champion)owner);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            FaceDirection(other1, targetPosEnd);
            targetPosEnd = GetPointByUnitFacingOffset(other1, 1200, 0);
            teamID = GetTeamID(owner);
            SpellCast((ObjAIBase)owner, default, targetPosEnd, targetPosEnd, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, targetPosStart);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RumbleHeatDelay(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            IncPAR(owner, 0, PrimaryAbilityResourceType.Other);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RumbleCarpetBomb(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast((ObjAIBase)owner, owner, owner.Position, default, 2, SpellSlotType.ExtraSlots, level, true, false, false, true, false, true, targetPosStart);
        }
    }
}
namespace Buffs
{
    public class RumbleCarpetBomb : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "GalioRighteousGust",
            BuffTextureName = "",
        };
        public override void OnDeactivate(bool expired)
        {
            SpellBuffClear(owner, nameof(Buffs.RumbleCarpetBombEffect));
        }
    }
}