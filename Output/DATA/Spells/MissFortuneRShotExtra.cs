#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MissFortuneRShotExtra : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {25, 60, 95, 130, 165};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float attackDamage;
            float attackBonus;
            float abilityDamage;
            float damageToDeal;
            float ricochetDamage;
            Particle asdf; // UNUSED
            if(hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if(hitResult == HitResult.HIT_Dodge)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if(hitResult == HitResult.HIT_Miss)
            {
                hitResult = HitResult.HIT_Normal;
            }
            teamID = GetTeamID(attacker);
            attackDamage = GetTotalAttackDamage(attacker);
            attackBonus = 0.75f * attackDamage;
            abilityDamage = this.effect0[level];
            damageToDeal = attackBonus + abilityDamage;
            ricochetDamage = damageToDeal * 1.15f;
            ApplyDamage(attacker, target, ricochetDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0.65f, 0, false, true, attacker);
            SpellEffectCreate(out asdf, out _, "missFortune_richochet_tar_second_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            SpellEffectCreate(out asdf, out _, "missFortune_richochet_tar_second.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
        }
    }
}