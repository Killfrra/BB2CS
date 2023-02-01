#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SadMummyBandageToss : BBBuffScript
    {
        Particle particleID;
        public SadMummyBandageToss(Particle particleID = default)
        {
            this.particleID = particleID;
        }
        public override void OnActivate()
        {
            object nextBuffVars_ParticleID;
            object nextBuffVars_WillRemove;
            //RequireVar(nextBuffVars_ParticleID);
            //RequireVar(nextBuffVars_WillRemove);
            PlayAnimation("Spell2", 0, owner, true, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            SpellEffectRemove(this.particleID);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            if(attacker is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, attacker);
            }
        }
    }
}
namespace Spells
{
    public class SadMummyBandageToss : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 18f, 16f, 14f, 12f, 10f, },
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 140, 200, 260, 320};
        int[] effect1 = {80, 140, 200, 260, 320};
        int[] effect2 = {80, 140, 200, 260, 320};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            canMove = GetCanMove(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            bool isStealthed;
            float distance;
            Vector3 landPos;
            bool nextBuffVars_WillRemove;
            Particle nextBuffVars_ParticleID;
            Particle gfasdf; // UNUSED
            bool canSee;
            teamID = GetTeamID(attacker);
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                distance = DistanceBetweenObjects("Target", "Attacker");
                landPos = GetUnitPosition(target);
                nextBuffVars_WillRemove = false;
                SpellEffectCreate(out nextBuffVars_ParticleID, out _, "Bandage_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spine", default, target, "R_hand", default, true, default, default, false, false);
                Move(attacker, landPos, 1350, 5, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
                AddBuff((ObjAIBase)target, attacker, new Buffs.SadMummyBandageToss(nextBuffVars_ParticleID), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false, attacker);
                ApplyStun(attacker, target, 1);
                SpellEffectCreate(out gfasdf, out _, "BandageToss_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if(target is Champion)
                {
                    distance = DistanceBetweenObjects("Target", "Attacker");
                    landPos = GetUnitPosition(target);
                    nextBuffVars_WillRemove = false;
                    SpellEffectCreate(out nextBuffVars_ParticleID, out _, "Bandage_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spine", default, target, "R_hand", default, true, default, default, false, false);
                    Move(attacker, landPos, 1350, 5, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
                    AddBuff((ObjAIBase)target, attacker, new Buffs.SadMummyBandageToss(nextBuffVars_ParticleID), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false, attacker);
                    ApplyStun(attacker, target, 1);
                    SpellEffectCreate(out gfasdf, out _, "BandageToss_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        distance = DistanceBetweenObjects("Target", "Attacker");
                        landPos = GetUnitPosition(target);
                        nextBuffVars_WillRemove = false;
                        SpellEffectCreate(out nextBuffVars_ParticleID, out _, "Bandage_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "spine", default, target, "R_hand", default, true, default, default, false, false);
                        Move(attacker, landPos, 1350, 5, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
                        AddBuff((ObjAIBase)target, attacker, new Buffs.SadMummyBandageToss(nextBuffVars_ParticleID), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        ApplyDamage(attacker, target, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false, attacker);
                        ApplyStun(attacker, target, 1);
                        SpellEffectCreate(out gfasdf, out _, "BandageToss_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
        }
    }
}