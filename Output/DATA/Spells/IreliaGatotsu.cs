#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IreliaGatotsu : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "IreliaGatotsu",
            BuffTextureName = "Irelia_Bladesurge.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            Particle placeholder; // UNUSED
            Particle castParticle; // UNUSED
            if(attacker.IsDead)
            {
                SpellEffectCreate(out placeholder, out _, "irelia_gotasu_ability_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out castParticle, out _, "irelia_gotasu_mana_refresh.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                IncPAR(owner, 35, PrimaryAbilityResourceType.MANA);
            }
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}
namespace Spells
{
    public class IreliaGatotsu : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitEnemies = 1,
                CanHitFriends = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 10, 10, 10, 10, 10, },
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {20, 50, 80, 110, 140};
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
            else if(!canCast)
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
            Particle smokeBomb; // UNUSED
            Vector3 ownerPos;
            Particle p3; // UNUSED
            Vector3 targetPos;
            float moveSpeed;
            float dashSpeed;
            float distance;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Distance;
            float nextBuffVars_dashSpeed;
            float nextBuffVars_DamageVar;
            float damageVar;
            float baseDamage;
            SpellEffectCreate(out smokeBomb, out _, "irelia_gotasu_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out p3, out _, "irelia_gotasu_cast_01.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out p3, out _, "irelia_gotasu_cast_02.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            targetPos = GetCastSpellTargetPos();
            moveSpeed = GetMovementSpeed(owner);
            dashSpeed = moveSpeed + 1400;
            distance = DistanceBetweenObjects("Owner", "Target");
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Distance = distance;
            nextBuffVars_dashSpeed = dashSpeed;
            damageVar = this.effect0[level];
            baseDamage = GetBaseAttackDamage(owner);
            nextBuffVars_DamageVar = damageVar + baseDamage;
            AddBuff((ObjAIBase)target, owner, new Buffs.IreliaGatotsuDash(nextBuffVars_TargetPos, nextBuffVars_Distance, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
    }
}