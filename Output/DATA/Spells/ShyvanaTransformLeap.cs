#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShyvanaTransformLeap : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {25000, 25000, 25000};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            float distance; // UNUSED
            targetPos = GetCastSpellTargetPos();
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaTransform(), 1, 1, this.effect0[level], BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            nextBuffVars_TargetPos = targetPos;
            AddBuff((ObjAIBase)target, owner, new Buffs.ShyvanaTransformLeap(nextBuffVars_TargetPos), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
            distance = DistanceBetweenObjectAndPoint(owner, targetPos);
            Move(owner, targetPos, 1100, 10, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
    }
}
namespace Buffs
{
    public class ShyvanaTransformLeap : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVCataclysm",
            BuffTextureName = "JarvanIV_Cataclysm.dds",
        };
        Vector3 targetPos;
        bool hasDealtDamage; // UNUSED
        bool hasCreatedRing; // UNUSED
        Particle selfParticle;
        Particle selfParticle2;
        Particle selfParticle11;
        Particle selfParticle12;
        Particle selfParticle3;
        Particle selfParticle4;
        Particle selfParticle5;
        Particle selfParticle6;
        Particle selfParticle7;
        Particle selfParticle8;
        Particle selfParticle9;
        Particle selfParticle10;
        int doOnce;
        int[] effect0 = {200, 300, 400};
        public ShyvanaTransformLeap(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.targetPos);
            this.hasDealtDamage = false;
            this.hasCreatedRing = false;
            SetCanCast(owner, false);
            teamID = GetTeamID(owner);
            PlayAnimation("Spell4", 0, owner, true, false, true);
            SpellEffectCreate(out this.selfParticle, out _, "shyvana_R_fire_skin.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle2, out _, "shyvana_ult_cast_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle11, out _, "shyvana_ult_cast_02_firefill.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_forearm", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle12, out _, "shyvana_ult_cast_02_firefill.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_forearm", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle3, out _, "shyvana_ult_cast_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle4, out _, "shyvana_ult_cast_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_shoulder", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle5, out _, "shyvana_ult_cast_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_shoulder", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle6, out _, "shyvana_ult_cast_02_arm.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_forearm", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle7, out _, "shyvana_ult_cast_02_arm.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_forearm", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle8, out _, "shyvana_ult_cast_02_tail.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "Tail_g", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle9, out _, "shyvana_ult_cast_02_hand.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.selfParticle10, out _, "shyvana_ult_cast_02_hand.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_hand", default, target, default, default, true, false, false, false, false);
            this.doOnce = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanCast(owner, true);
            UnlockAnimation(owner, true);
            SpellEffectRemove(this.selfParticle);
            SpellEffectRemove(this.selfParticle2);
            SpellEffectRemove(this.selfParticle3);
            SpellEffectRemove(this.selfParticle4);
            SpellEffectRemove(this.selfParticle5);
            SpellEffectRemove(this.selfParticle6);
            SpellEffectRemove(this.selfParticle7);
            SpellEffectRemove(this.selfParticle8);
            SpellEffectRemove(this.selfParticle9);
            SpellEffectRemove(this.selfParticle10);
            SpellEffectRemove(this.selfParticle11);
            SpellEffectRemove(this.selfParticle12);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos;
            float distance;
            targetPos = this.targetPos;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Vector3 position;
                float nextBuffVars_Gravity;
                float nextBuffVars_Speed;
                Vector3 nextBuffVars_Position;
                float nextBuffVars_IdealDistance; // UNUSED
                distance = DistanceBetweenObjectAndPoint(unit, targetPos);
                if(distance > 275)
                {
                    distance = 275;
                }
                FaceDirection(unit, targetPos);
                position = GetPointByUnitFacingOffset(unit, distance, 0);
                nextBuffVars_Gravity = 10;
                nextBuffVars_Speed = 1000;
                nextBuffVars_Position = position;
                nextBuffVars_IdealDistance = distance;
                if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.ShyvanaTransformCheck)) == 0)
                {
                    int level;
                    AddBuff(attacker, unit, new Buffs.ShyvanaTransformCheck(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.ShyvanaTransformDamage(nextBuffVars_Gravity, nextBuffVars_Speed, nextBuffVars_Position), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
            }
            distance = DistanceBetweenObjectAndPoint(owner, targetPos);
            if(distance <= 400)
            {
                if(this.doOnce == 0)
                {
                    UnlockAnimation(owner, true);
                    PlayAnimation("Spell4_land", 0, owner, false, true, true);
                    this.doOnce = 1;
                }
            }
        }
        public override void OnMoveEnd()
        {
            SetCanCast(owner, true);
            SpellBuffRemove(owner, nameof(Buffs.ShyvanaTransformLeap), (ObjAIBase)owner, 0);
        }
        public override void OnMoveSuccess()
        {
            Vector3 centerPos; // UNUSED
            TeamId teamID;
            Particle groundhit; // UNUSED
            centerPos = GetUnitPosition(owner);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out groundhit, out _, "shyvana_ult_impact_01.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weapon_C", default, target, default, default, true, false, false, false, false);
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 125, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                FaceDirection(owner, unit.Position);
            }
        }
    }
}