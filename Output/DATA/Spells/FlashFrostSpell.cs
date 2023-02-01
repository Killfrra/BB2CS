#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FlashFrostSpell : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 90, 120, 150, 180};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MovementSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            BreakSpellShields(target);
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
            nextBuffVars_MovementSpeedMod = -0.2f;
            nextBuffVars_AttackSpeedMod = 0;
            AddBuff(attacker, target, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
        }
    }
}