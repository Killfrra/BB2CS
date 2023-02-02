#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MissileBarrageMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
        };
        int[] effect0 = {120, 190, 260};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            bool isStealthed;
            float baseDamage;
            float totalAttackDamage;
            float bonusAttackDamage;
            float damageAmount;
            Particle part; // UNUSED
            Vector3 targetPos;
            teamID = GetTeamID(owner);
            isStealthed = GetStealthed(target);
            baseDamage = this.effect0[level];
            totalAttackDamage = GetTotalAttackDamage(owner);
            bonusAttackDamage = 0.2f * totalAttackDamage;
            damageAmount = bonusAttackDamage + baseDamage;
            if(!isStealthed)
            {
                SpellEffectCreate(out part, out _, "corki_MissleBarrage_std_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", target.Position, target, default, default, true, default, default, false, false);
                targetPos = GetUnitPosition(target);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
                }
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if(target is Champion)
                {
                    SpellEffectCreate(out part, out _, "corki_MissleBarrage_std_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", target.Position, target, default, default, true, default, default, false, false);
                    targetPos = GetUnitPosition(target);
                    foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
                    }
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    bool canSee;
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        SpellEffectCreate(out part, out _, "corki_MissleBarrage_std_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", target.Position, target, default, default, true, default, default, false, false);
                        targetPos = GetUnitPosition(target);
                        foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                        {
                            BreakSpellShields(unit);
                            ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
                        }
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
        }
    }
}