#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class IreliaEquilibriumStrike : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 130, 180, 230, 280};
        float[] effect1 = {1, 1.25f, 1.5f, 1.75f, 2};
        float[] effect2 = {1, 1.25f, 1.5f, 1.75f, 2};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float targetPercent;
            float selfPercent;
            Particle a; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            targetPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
            selfPercent = GetHealthPercent(attacker, PrimaryAbilityResourceType.MANA);
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
            if(targetPercent >= selfPercent)
            {
                ApplyStun(attacker, target, this.effect1[level]);
                SpellEffectCreate(out a, out _, "irelia_equilibriumStrike_tar_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            }
            else
            {
                nextBuffVars_MoveSpeedMod = -0.6f;
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, this.effect2[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                SpellEffectCreate(out a, out _, "irelia_equilibriumStrike_tar_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            }
        }
    }
}