#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MonkeyKingNimbus : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
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
        float[] effect0 = {0.3f, 0.35f, 0.4f, 0.45f, 0.5f};
        int[] effect1 = {60, 105, 150, 195, 240};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Vector3 ownerPos;
            Particle p3; // UNUSED
            Vector3 targetPos;
            float moveSpeed;
            float dashSpeed;
            float distance;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Distance;
            float nextBuffVars_dashSpeed;
            float nextBuffVars_AttackSpeedVar;
            float nextBuffVars_DamageVar;
            float damageVar;
            float bonusAD;
            float bonusDamage;
            float unitsHit;
            bool isStealthed;
            bool canSee;
            Minion other1;
            teamID = GetTeamID(owner);
            ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out p3, out _, "monkeyKing_Q_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out p3, out _, "monkeyKing_Q_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            targetPos = GetCastSpellTargetPos();
            moveSpeed = GetMovementSpeed(owner);
            dashSpeed = moveSpeed + 1050;
            distance = DistanceBetweenObjects("Owner", "Target");
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Distance = distance;
            nextBuffVars_dashSpeed = dashSpeed;
            nextBuffVars_AttackSpeedVar = this.effect0[level];
            damageVar = this.effect1[level];
            bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusDamage = bonusAD * 0.8f;
            nextBuffVars_DamageVar = bonusDamage + damageVar;
            AddBuff((ObjAIBase)target, owner, new Buffs.MonkeyKingNimbusKick(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_AttackSpeedVar, nextBuffVars_DamageVar), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonkeyKingDecoyStealth)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.MonkeyKingDecoyStealth), (ObjAIBase)owner, 0);
            }
            unitsHit = 0;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, target.Position, 320, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 10, default, true))
            {
                if(unitsHit < 2)
                {
                    if(unit != target)
                    {
                        isStealthed = GetStealthed(unit);
                        if(isStealthed)
                        {
                            canSee = CanSeeTarget(owner, unit);
                            if(canSee)
                            {
                                targetPos = GetUnitPosition(unit);
                                nextBuffVars_TargetPos = targetPos;
                                other1 = SpawnMinion("MonkeyKingClone", "MonkeyKingFlying", "Aggro.lua", ownerPos, teamID, false, false, false, false, false, true, 0, false, false, (Champion)owner);
                                AddBuff((ObjAIBase)unit, other1, new Buffs.MonkeyKingNimbusKickClone(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
                                unitsHit++;
                            }
                        }
                        else
                        {
                            targetPos = GetUnitPosition(unit);
                            nextBuffVars_TargetPos = targetPos;
                            other1 = SpawnMinion("MonkeyKingClone", "MonkeyKingFlying", "Aggro.lua", ownerPos, teamID, false, false, false, false, false, true, 0, false, false, (Champion)owner);
                            AddBuff((ObjAIBase)unit, other1, new Buffs.MonkeyKingNimbusKickClone(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
                            unitsHit++;
                        }
                    }
                }
            }
        }
    }
}