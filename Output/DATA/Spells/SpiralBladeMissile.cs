#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SpiralBladeMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {70, 115, 160, 205, 250};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float percentOfAttack;
            float totalDamage;
            float baseDamage;
            float bonusDamage;
            float aP;
            float aPDamage;
            float damageToDeal;
            Particle afa; // UNUSED
            teamID = GetTeamID(owner);
            percentOfAttack = charVars.PercentOfAttack;
            totalDamage = GetTotalAttackDamage(owner);
            baseDamage = GetBaseAttackDamage(owner);
            bonusDamage = totalDamage - baseDamage;
            bonusDamage *= 1.1f;
            aP = GetFlatMagicDamageMod(owner);
            aPDamage = 0.5f * aP;
            damageToDeal = bonusDamage + aPDamage;
            ApplyDamage(attacker, target, damageToDeal + this.effect0[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0, 0, false, false, attacker);
            charVars.PercentOfAttack *= 0.8f;
            charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.4f);
            SpellEffectCreate(out afa, out _, "SpiralBlade_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, false, false, false, false);
        }
    }
}