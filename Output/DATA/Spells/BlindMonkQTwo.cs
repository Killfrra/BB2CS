#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BlindMonkQTwo : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {50, 80, 110, 140, 170};
        int[] effect1 = {50, 80, 110, 140, 170};
        public override bool CanCast()
        {
            bool returnValue = true;
            TeamId teamID;
            returnValue = false;
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.BlindMonkQOne), true))
                {
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.BlindMonkQOne)) > 0)
                    {
                        returnValue = true;
                    }
                }
            }
            else
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.BlindMonkQOneChaos), true))
                {
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.BlindMonkQOneChaos)) > 0)
                    {
                        returnValue = true;
                    }
                }
            }
            return returnValue;
        }
        public override void SelfExecute()
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
            float nextBuffVars_DamageVar;
            float baseDamage;
            float bonusAD;
            float damageVar;
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 2, nameof(Buffs.BlindMonkQOne), true))
                {
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.BlindMonkQOne)) > 0)
                    {
                        SpellBuffRemove(unit, nameof(Buffs.BlindMonkQOne), (ObjAIBase)owner, 0);
                        ownerPos = GetUnitPosition(owner);
                        SpellEffectCreate(out p3, out _, "blindMonk_Q_resonatingStrike_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                        targetPos = GetUnitPosition(unit);
                        moveSpeed = GetMovementSpeed(owner);
                        dashSpeed = moveSpeed + 1350;
                        distance = DistanceBetweenObjects("Owner", "Unit");
                        nextBuffVars_TargetPos = targetPos;
                        nextBuffVars_Distance = distance;
                        nextBuffVars_dashSpeed = dashSpeed;
                        baseDamage = this.effect0[level];
                        bonusAD = GetFlatPhysicalDamageMod(owner);
                        bonusAD *= 0.9f;
                        damageVar = baseDamage + bonusAD;
                        nextBuffVars_DamageVar = damageVar;
                        AddBuff((ObjAIBase)unit, owner, new Buffs.BlindMonkQTwoDash(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                        SpellBuffClear(owner, nameof(Buffs.BlindMonkQManager));
                    }
                }
            }
            else
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 2, nameof(Buffs.BlindMonkQOneChaos), true))
                {
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.BlindMonkQOneChaos)) > 0)
                    {
                        SpellBuffRemove(unit, nameof(Buffs.BlindMonkQOneChaos), (ObjAIBase)owner, 0);
                        ownerPos = GetUnitPosition(owner);
                        SpellEffectCreate(out p3, out _, "blindMonk_Q_resonatingStrike_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                        targetPos = GetUnitPosition(unit);
                        moveSpeed = GetMovementSpeed(owner);
                        dashSpeed = moveSpeed + 1350;
                        distance = DistanceBetweenObjects("Owner", "Unit");
                        nextBuffVars_TargetPos = targetPos;
                        nextBuffVars_Distance = distance;
                        nextBuffVars_dashSpeed = dashSpeed;
                        baseDamage = this.effect1[level];
                        bonusAD = GetFlatPhysicalDamageMod(owner);
                        bonusAD *= 0.9f;
                        damageVar = baseDamage + bonusAD;
                        nextBuffVars_DamageVar = damageVar;
                        AddBuff((ObjAIBase)unit, owner, new Buffs.BlindMonkQTwoDash(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                        SpellBuffClear(owner, nameof(Buffs.BlindMonkQManager));
                    }
                }
            }
        }
    }
}