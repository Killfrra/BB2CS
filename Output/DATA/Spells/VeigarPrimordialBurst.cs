#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VeigarPrimordialBurst : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 16f, 14f, 12f, 10f, 8f, },
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {250, 375, 500};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float targetAP;
            float totalDamage;
            targetAP = GetFlatMagicDamageMod(target);
            targetAP *= 0.8f;
            totalDamage = this.effect0[level];
            totalDamage += targetAP;
            ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1.2f, 1, false, false, attacker);
        }
    }
}