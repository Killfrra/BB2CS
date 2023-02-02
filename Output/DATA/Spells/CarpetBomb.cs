#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CarpetBomb : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float tickDuration; // UNUSED
        int[] effect0 = {15, 16, 17, 18, 19};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            if(!canCast)
            {
                returnValue = false;
            }
            if(!canMove)
            {
                returnValue = false;
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
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
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            float tickAmount;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            Vector3 nextBuffVars_TargetPos;
            object damage; // UNITIALIZED
            float moveSpeed;
            float slashSpeed;
            float duration;
            float nextBuffVars_SelfAP;
            object nextBuffVars_Damage; // UNUSED
            bool nextBuffVars_WillRemove;
            bool nextBuffVars_WillMove;
            float nextBuffVars_SlashSpeed;
            tickAmount = this.effect0[level];
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 800)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 800, 0);
            }
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_SelfAP = GetFlatMagicDamageMod(owner);
            nextBuffVars_Damage = damage;
            nextBuffVars_WillRemove = false;
            ownerPos = GetUnitPosition(owner);
            moveSpeed = GetMovementSpeed(owner);
            slashSpeed = moveSpeed + 650;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            duration = distance / slashSpeed;
            this.tickDuration = duration / tickAmount;
            nextBuffVars_WillMove = true;
            nextBuffVars_SlashSpeed = slashSpeed;
            AddBuff(attacker, owner, new Buffs.CarpetBomb(nextBuffVars_WillMove, nextBuffVars_TargetPos, nextBuffVars_SelfAP, nextBuffVars_WillRemove, nextBuffVars_SlashSpeed), 1, 1, 0.05f + duration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, owner, new Buffs.ValkyrieSound(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class CarpetBomb : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            SpellFXOverrideSkins = new[]{ "UrfRiderCorki", },
        };
        bool willMove;
        Vector3 targetPos;
        float selfAP;
        bool willRemove;
        float slashSpeed;
        Particle particle;
        float tickDuration;
        float lastTimeExecuted;
        int[] effect0 = {30, 45, 60, 75, 90};
        public CarpetBomb(bool willMove = default, Vector3 targetPos = default, float selfAP = default, bool willRemove = default, float slashSpeed = default, float tickDuration = default)
        {
            this.willMove = willMove;
            this.targetPos = targetPos;
            this.selfAP = selfAP;
            this.willRemove = willRemove;
            this.slashSpeed = slashSpeed;
            this.tickDuration = tickDuration;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            //RequireVar(this.willMove);
            //RequireVar(this.targetPos);
            //RequireVar(this.selfAP);
            //RequireVar(this.willRemove);
            //RequireVar(this.slashSpeed);
            targetPos = this.targetPos;
            Move(owner, targetPos, this.slashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out this.particle, out _, "corki_valkrie_speed.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, default, default, false);
            PlayAnimation("Spell2", 0, owner, true, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, false);
            if(!this.willMove)
            {
                SpellEffectRemove(this.particle);
            }
        }
        public override void OnUpdateStats()
        {
            float moveSpeedVal;
            moveSpeedVal = GetMovementSpeed(owner);
            if(moveSpeedVal < 300)
            {
                float moveSpeedDif;
                moveSpeedDif = 300 - moveSpeedVal;
                IncFlatMovementSpeedMod(owner, moveSpeedDif);
            }
        }
        public override void OnUpdateActions()
        {
            int level;
            float tickDuration;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            tickDuration = this.tickDuration;
            if(ExecutePeriodically(0, ref this.lastTimeExecuted, true, tickDuration))
            {
                float damagePerTick;
                float aPBonus;
                float nextBuffVars_DamagePerTick;
                Vector3 bombPos;
                TeamId teamOfOwner;
                Minion other3;
                damagePerTick = this.effect0[level];
                aPBonus = 0.2f * this.selfAP;
                damagePerTick += aPBonus;
                nextBuffVars_DamagePerTick = damagePerTick;
                bombPos = GetUnitPosition(owner);
                teamOfOwner = GetTeamID(owner);
                other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", bombPos, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, false, 0, default, true, (Champion)owner);
                AddBuff(attacker, other3, new Buffs.DangerZone(nextBuffVars_DamagePerTick), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
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