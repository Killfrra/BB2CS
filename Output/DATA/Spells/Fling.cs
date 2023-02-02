#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Fling : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {100, 150, 200, 250, 300};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 targetPos;
            Vector3 landPos;
            float distance;
            float delayTimer;
            targetPos = GetUnitPosition(target);
            landPos = GetPointByUnitFacingOffset(owner, 420, 180);
            distance = DistanceBetweenPoints(targetPos, landPos);
            delayTimer = distance / 1160;
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.Fling(), 1, 1, delayTimer, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class Fling : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        bool hasHitGround; // UNUSED
        public override void OnActivate()
        {
            Vector3 landPos;
            this.hasHitGround = false;
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
            landPos = GetPointByUnitFacingOffset(attacker, 420, 180);
            Move(owner, landPos, 1000, 60, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 420);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Particle arrr; // UNUSED
            teamID = GetTeamID(attacker);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            SpellEffectCreate(out arrr, out _, "fling_land.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
        }
    }
}