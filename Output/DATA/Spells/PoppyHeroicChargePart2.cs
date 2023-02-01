#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyHeroicChargePart2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "HeroicCharge_cas2.troy", },
            BuffTextureName = "Poppy_HeroicCharge.dds",
        };
        float slashSpeed;
        Vector3 newTargetPos;
        float damageTwo;
        bool willRemove; // UNUSED
        bool willMove; // UNUSED
        Particle particleCharge2;
        int[] effect0 = {75, 125, 175, 225, 275};
        public PoppyHeroicChargePart2(float slashSpeed = default, Vector3 newTargetPos = default, float damageTwo = default)
        {
            this.slashSpeed = slashSpeed;
            this.newTargetPos = newTargetPos;
            this.damageTwo = damageTwo;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            ObjAIBase caster;
            int level;
            Vector3 newTargetPos;
            teamID = GetTeamID(attacker);
            //RequireVar(this.damageTwo);
            //RequireVar(this.newTargetPos);
            //RequireVar(this.slashSpeed);
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel(caster, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            newTargetPos = this.newTargetPos;
            this.damageTwo = this.effect0[level];
            this.willRemove = false;
            this.willMove = true;
            SetGhosted(owner, true);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            Move(owner, newTargetPos, this.slashSpeed, 0, 0, ForceMovementType.FIRST_COLLISION_HIT, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out this.particleCharge2, out _, "HeroicCharge_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            ApplyAssistMarker(caster, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleCharge2);
            SetGhosted(owner, false);
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
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            TeamId teamID;
            float distanceVar;
            Particle targetParticle; // UNUSED
            caster = SetBuffCasterUnit();
            teamID = GetTeamID(caster);
            distanceVar = DistanceBetweenObjectAndPoint(owner, this.newTargetPos);
            if(distanceVar >= 75)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PoppyHeroicChargePart2Check)) == 0)
                {
                    if(owner != caster)
                    {
                        BreakSpellShields(owner);
                        SpellEffectCreate(out targetParticle, out _, "HeroicCharge_tar2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                        ApplyDamage(caster, owner, this.damageTwo, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                        ApplyStun(caster, owner, 1.5f);
                        if(owner is Champion)
                        {
                            IssueOrder(caster, OrderType.AttackTo, default, owner);
                        }
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyHeroicChargePart2Check(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
    }
}