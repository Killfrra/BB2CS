#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class DeathfireGraspSpell : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float ap;
            float apMod;
            float percentBurn;
            float curHealth;
            float damageToDeal;
            ap = GetFlatMagicDamageMod(owner);
            apMod = 0.00035f * ap;
            percentBurn = 0.3f + apMod;
            curHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
            damageToDeal = percentBurn * curHealth;
            damageToDeal = Math.Max(damageToDeal, 200);
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, default, false, false);
        }
    }
}