#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CaitlynPiltoverPeacemaker : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {20, 60, 100, 140, 180};
        int[] effect1 = {20, 60, 100, 140, 180};
        int[] effect2 = {20, 60, 100, 140, 180};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            bool isStealthed;
            float percentOfAttack;
            float baseDamage;
            Particle asdf; // UNUSED
            bool canSee;
            teamID = GetTeamID(owner);
            isStealthed = GetStealthed(target);
            hitResult = HitResult.HIT_Normal;
            percentOfAttack = charVars.PercentOfAttack;
            baseDamage = GetTotalAttackDamage(owner);
            baseDamage *= 1.3f;
            if(!isStealthed)
            {
                BreakSpellShields(target);
                SpellEffectCreate(out asdf, out _, "caitlyn_peaceMaker_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                ApplyDamage(attacker, target, baseDamage + this.effect0[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0, 0, false, true, attacker);
                charVars.PercentOfAttack *= 0.85f;
                charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.4f);
            }
            else
            {
                if(target is Champion)
                {
                    BreakSpellShields(target);
                    SpellEffectCreate(out asdf, out _, "caitlyn_peaceMaker_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                    ApplyDamage(attacker, target, baseDamage + this.effect1[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0, 0, false, true, attacker);
                    charVars.PercentOfAttack *= 0.85f;
                    charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.4f);
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        BreakSpellShields(target);
                        SpellEffectCreate(out asdf, out _, "caitlyn_peaceMaker_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                        ApplyDamage(attacker, target, baseDamage + this.effect2[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0, 0, false, true, attacker);
                        charVars.PercentOfAttack *= 0.85f;
                        charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.4f);
                    }
                }
            }
        }
    }
}