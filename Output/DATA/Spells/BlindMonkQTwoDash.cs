#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkQTwoDash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
            IsDeathRecapSource = true,
        };
        Vector3 targetPos;
        float dashSpeed;
        float damageVar;
        Particle selfParticle;
        bool willRemove;
        public BlindMonkQTwoDash(Vector3 targetPos = default, float dashSpeed = default, float damageVar = default)
        {
            this.targetPos = targetPos;
            this.dashSpeed = dashSpeed;
            this.damageVar = damageVar;
        }
        public override void OnActivate()
        {
            Vector3 targetPos; // UNUSED
            float distance;
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.damageVar);
            targetPos = this.targetPos;
            distance = DistanceBetweenObjects("Attacker", "Owner");
            MoveToUnit(owner, attacker, this.dashSpeed, 0, ForceMovementOrdersType.CANCEL_ORDER, 0, 2000, distance, 0);
            SpellEffectCreate(out this.selfParticle, out _, "blindMonk_Q_resonatingStrike_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            PlayAnimation("Spell1b", 0, owner, true, false, true);
            this.willRemove = false;
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.selfParticle);
            UnlockAnimation(owner, true);
            SetGhosted(owner, false);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnMoveEnd()
        {
            this.willRemove = true;
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            float casterHealth;
            float healthPercent;
            float missingHealthPerc;
            float missingHealth;
            float bonusDamage;
            TeamId casterID;
            caster = SetBuffCasterUnit();
            casterHealth = GetMaxHealth(caster, PrimaryAbilityResourceType.MANA);
            healthPercent = GetHealthPercent(caster, PrimaryAbilityResourceType.MANA);
            missingHealthPerc = 1 - healthPercent;
            missingHealth = casterHealth * missingHealthPerc;
            bonusDamage = 0.1f * missingHealth;
            casterID = GetTeamID(caster);
            if(casterID == TeamId.TEAM_NEUTRAL)
            {
                bonusDamage = Math.Min(bonusDamage, 400);
            }
            BreakSpellShields(caster);
            this.damageVar += bonusDamage;
            AddBuff((ObjAIBase)owner, caster, new Buffs.BlindMonkQTwoDashParticle(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage((ObjAIBase)owner, caster, this.damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, (ObjAIBase)owner);
            SpellBuffRemoveCurrent(owner);
            if(owner.Team != caster.Team)
            {
                if(caster is Champion)
                {
                    IssueOrder(owner, OrderType.AttackTo, default, caster);
                }
            }
        }
    }
}