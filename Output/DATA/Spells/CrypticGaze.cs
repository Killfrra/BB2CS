#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CrypticGaze : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 12f, 11f, 10f, 9f, 8f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {70, 125, 180, 240, 300};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0.9f, 0, false, false, attacker);
            ApplyStun(attacker, target, 1.5f);
        }
    }
}