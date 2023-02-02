#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Slash : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {70, 100, 130, 160, 190};
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
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            float baseAbilityDamage;
            float totalDamage;
            float baseDamage;
            float bonusDamage;
            float abilityPower;
            Vector3 ownerPos;
            float slashSpeed;
            float distance;
            float duration;
            float nextBuffVars_Damage;
            bool nextBuffVars_WillRemove;
            bool nextBuffVars_WillMove;
            float nextBuffVars_SlashSpeed;
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_TargetPos = targetPos;
            baseAbilityDamage = this.effect0[level];
            totalDamage = GetTotalAttackDamage(owner);
            baseDamage = GetBaseAttackDamage(owner);
            bonusDamage = totalDamage - baseDamage;
            bonusDamage *= 1.2f;
            abilityPower = GetFlatMagicDamageMod(owner);
            bonusDamage += abilityPower;
            nextBuffVars_Damage = baseAbilityDamage + bonusDamage;
            nextBuffVars_WillRemove = false;
            ownerPos = GetUnitPosition(owner);
            slashSpeed = 900;
            slashSpeed = Math.Max(slashSpeed, 425);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            duration = distance / slashSpeed;
            nextBuffVars_WillMove = true;
            nextBuffVars_SlashSpeed = slashSpeed;
            AddBuff(attacker, owner, new Buffs.Slash(nextBuffVars_Damage, nextBuffVars_WillMove, nextBuffVars_TargetPos, nextBuffVars_WillRemove, nextBuffVars_SlashSpeed), 1, 1, 0.05f + duration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Slash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        float damage;
        bool willMove;
        Vector3 targetPos;
        bool willRemove;
        float slashSpeed;
        Particle particle;
        float lastTimeExecuted;
        public Slash(float damage = default, bool willMove = default, Vector3 targetPos = default, bool willRemove = default, float slashSpeed = default)
        {
            this.damage = damage;
            this.willMove = willMove;
            this.targetPos = targetPos;
            this.willRemove = willRemove;
            this.slashSpeed = slashSpeed;
        }
        public override void OnCollision()
        {
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SlashBeenHit)) == 0)
            {
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        Particle particle; // UNUSED
                        AddBuff((ObjAIBase)owner, target, new Buffs.SlashBeenHit(), 1, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(target);
                        ApplyDamage(attacker, target, this.damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, true, attacker);
                        SpellEffectCreate(out particle, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false, false);
                        StartTrackingCollisions(owner, true);
                        if(target is Champion)
                        {
                            IncPAR(owner, 5, PrimaryAbilityResourceType.Other);
                        }
                        else
                        {
                            IncPAR(owner, 2, PrimaryAbilityResourceType.Other);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            //RequireVar(this.willMove);
            //RequireVar(this.targetPos);
            //RequireVar(this.damage);
            //RequireVar(this.willRemove);
            //RequireVar(this.slashSpeed);
            targetPos = this.targetPos;
            PlayAnimation("Spell3", 0, owner, true, false, true);
            Move(owner, targetPos, this.slashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            StartTrackingCollisions(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            StartTrackingCollisions(owner, true);
            if(!this.willMove)
            {
                SpellEffectRemove(this.particle);
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.1f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.SlashBeenHit), false))
                {
                    Particle particle; // UNUSED
                    if(unit is ObjAIBase)
                    {
                        if(unit is not BaseTurret)
                        {
                        }
                    }
                    AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, this.damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, true, attacker);
                    SpellEffectCreate(out particle, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false, false);
                    if(unit is Champion)
                    {
                        IncPAR(owner, 5, PrimaryAbilityResourceType.Other);
                    }
                    else
                    {
                        IncPAR(owner, 2, PrimaryAbilityResourceType.Other);
                    }
                }
            }
            if(this.willMove)
            {
                SpellEffectCreate(out this.particle, out _, "Slash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
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
    }
}