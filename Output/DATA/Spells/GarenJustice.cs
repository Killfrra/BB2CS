#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GarenJustice : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        Particle particle; // UNUSED
        Particle particle2; // UNUSED
        Particle particle3; // UNUSED
        float[] effect0 = {3.5f, 3, 2.5f};
        int[] effect1 = {175, 350, 525};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float damageScale;
            float damage;
            float maxHP;
            float currentHP;
            float missingHP;
            float exeDmg;
            float finalDamage;
            damageScale = this.effect0[level];
            damage = this.effect1[level];
            maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            currentHP = GetHealth(target, PrimaryAbilityResourceType.MANA);
            SpellEffectCreate(out this.particle, out _, "garen_damacianJustice_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "C_BUFFBONE_GLB_CHEST_LOC", default, attacker, default, default, false);
            missingHP = maxHP - currentHP;
            exeDmg = missingHP / damageScale;
            finalDamage = exeDmg + damage;
            BreakSpellShields(target);
            ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
            SpellEffectCreate(out this.particle2, out _, "garen_damacianJustice_tar_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, false);
            SpellEffectCreate(out this.particle3, out _, "garen_damacianJustice_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, false);
        }
    }
}