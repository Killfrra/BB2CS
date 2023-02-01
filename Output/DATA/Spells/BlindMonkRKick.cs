#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkRKick : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "blindMonk_R_self_mis.troy", "", },
            BuffName = "BlindMonkDragonsRage",
            BuffTextureName = "BlindMonkR.dds",
        };
        float secondaryDamage;
        Vector3 tarPos;
        public BlindMonkRKick(float secondaryDamage = default, Vector3 tarPos = default)
        {
            this.secondaryDamage = secondaryDamage;
            this.tarPos = tarPos;
        }
        public override void OnActivate()
        {
            Vector3 tarPos;
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            //RequireVar(this.secondaryDamage);
            //RequireVar(this.tarPos);
            tarPos = this.tarPos;
            Move(target, tarPos, 1000, 5, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 800, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID;
            Particle pH; // UNUSED
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 160, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(unit != owner)
                {
                    if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.BlindMonkRMarker)) == 0)
                    {
                        AddBuff((ObjAIBase)unit, unit, new Buffs.BlindMonkRMarker(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(unit);
                        teamID = GetTeamID(owner);
                        SpellEffectCreate(out pH, out _, "blind_monk_ult_unit_impact.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        ApplyDamage(attacker, unit, this.secondaryDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
                        AddBuff(attacker, unit, new Buffs.BlindMonkRDamage(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, true);
                    }
                }
            }
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}
namespace Spells
{
    public class BlindMonkRKick : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitEnemies = 1,
                CanHitFriends = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 4, 4, 4, 4, 4, },
            },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {200, 400, 600, 600, 600};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            float bonusAD;
            float primaryDamage;
            float secondaryDamage;
            float nextBuffVars_SecondaryDamage;
            Vector3 nextBuffVars_TarPos;
            float distanceToAdd;
            float distanceToKick;
            Vector3 tarPos;
            baseDamage = this.effect0[level];
            bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD *= 2;
            primaryDamage = baseDamage + bonusAD;
            secondaryDamage = primaryDamage / 1;
            nextBuffVars_SecondaryDamage = secondaryDamage;
            distanceToAdd = DistanceBetweenObjects("Owner", "Target");
            distanceToKick = distanceToAdd + 800;
            FaceDirection(owner, target.Position);
            tarPos = GetPointByUnitFacingOffset(owner, distanceToKick, 0);
            nextBuffVars_TarPos = tarPos;
            AddBuff((ObjAIBase)owner, target, new Buffs.BlindMonkRKick(nextBuffVars_SecondaryDamage, nextBuffVars_TarPos), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
            ApplyDamage(attacker, target, primaryDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, true, attacker);
        }
    }
}