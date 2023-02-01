#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyHeroicCharge : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Alpha Striking",
            BuffTextureName = "MasterYi_LeapStrike.dds",
        };
        Vector3 targetPos;
        float damage;
        float damageTwo;
        bool willRemove;
        float slashSpeed;
        Particle particleCharge;
        bool willMove;
        public PoppyHeroicCharge(Vector3 targetPos = default, float damage = default, float damageTwo = default, bool willRemove = default, float slashSpeed = default, bool willMove = default)
        {
            this.targetPos = targetPos;
            this.damage = damage;
            this.damageTwo = damageTwo;
            this.willRemove = willRemove;
            this.slashSpeed = slashSpeed;
            this.willMove = willMove;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 targetPos;
            teamID = GetTeamID(owner);
            //RequireVar(this.targetPos);
            //RequireVar(this.damage);
            //RequireVar(this.damageTwo);
            //RequireVar(this.willRemove);
            //RequireVar(this.slashSpeed);
            targetPos = this.targetPos;
            Move(owner, targetPos, this.slashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out this.particleCharge, out _, "HeroicCharge_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            PlayAnimation("RunUlt", 0, owner, true, false, true);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            StopCurrentOverrideAnimation("RunUlt", owner, false);
            SpellEffectRemove(this.particleCharge);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PoppyHeroicChargePart2)) == 0)
            {
                SetCanAttack(owner, true);
            }
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
        public override void OnUpdateActions()
        {
            if(this.willMove)
            {
                this.willMove = false;
            }
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            TeamId teamID;
            ObjAIBase caster;
            Particle targetParticle; // UNUSED
            Vector3 newTargetPos;
            float nextBuffVars_SlashSpeed;
            Vector3 nextBuffVars_NewTargetPos;
            float nextBuffVars_DamageTwo;
            teamID = GetTeamID(owner);
            caster = SetBuffCasterUnit();
            if(GetBuffCountFromCaster(caster, caster, nameof(Buffs.PoppyHeroicChargePoppyFix)) > 0)
            {
                this.damageTwo += this.damage;
                BreakSpellShields(caster);
                ApplyDamage((ObjAIBase)owner, caster, this.damageTwo, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                ApplyStun(owner, caster, 1.5f);
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                SpellEffectCreate(out targetParticle, out _, "HeroicCharge_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                BreakSpellShields(caster);
                ApplyDamage((ObjAIBase)owner, caster, this.damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, (ObjAIBase)owner);
                newTargetPos = GetPointByUnitFacingOffset(owner, 400, 0);
                nextBuffVars_SlashSpeed = this.slashSpeed;
                nextBuffVars_NewTargetPos = newTargetPos;
                nextBuffVars_DamageTwo = this.damageTwo;
                AddBuff((ObjAIBase)owner, caster, new Buffs.PoppyHeroicChargePart2(nextBuffVars_SlashSpeed, nextBuffVars_NewTargetPos, nextBuffVars_DamageTwo), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
                UnlockAnimation(owner, false);
                IssueOrder(owner, OrderType.AttackTo, default, caster);
                if(GetBuffCountFromCaster(caster, default, nameof(Buffs.PoppyHeroicChargePart2)) > 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyHeroicChargePart2(nextBuffVars_SlashSpeed, nextBuffVars_NewTargetPos, nextBuffVars_DamageTwo), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}
namespace Spells
{
    public class PoppyHeroicCharge : BBSpellScript
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
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {50, 75, 100, 125, 150};
        int[] effect1 = {75, 125, 175, 225, 275};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            if(!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Damage;
            float nextBuffVars_DamageTwo;
            bool nextBuffVars_WillMove;
            bool nextBuffVars_WillRemove;
            float nextBuffVars_SlashSpeed;
            Vector3 ownerPos;
            float moveSpeed;
            float slashSpeed;
            float distance;
            float duration;
            targetPos = GetUnitPosition(target);
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Damage = this.effect0[level];
            nextBuffVars_DamageTwo = this.effect1[level];
            nextBuffVars_WillMove = true;
            nextBuffVars_WillRemove = false;
            ownerPos = GetUnitPosition(owner);
            moveSpeed = GetMovementSpeed(owner);
            slashSpeed = moveSpeed + 1200;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            duration = distance / slashSpeed;
            nextBuffVars_SlashSpeed = slashSpeed;
            AddBuff((ObjAIBase)target, owner, new Buffs.PoppyHeroicCharge(nextBuffVars_TargetPos, nextBuffVars_Damage, nextBuffVars_DamageTwo, nextBuffVars_WillRemove, nextBuffVars_SlashSpeed, nextBuffVars_WillMove), 1, 1, 0.25f + duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyHeroicChargePoppyFix(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}