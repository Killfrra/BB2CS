#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MonkeyKingDoubleAttackHit : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {0, 0, 0, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonkeyKingPassive)) > 0)
            {
                float tAD;
                float damageToDeal;
                tAD = GetTotalAttackDamage(owner);
                damageToDeal = 2 * tAD;
                ApplyDamage(attacker, target, damageToDeal + this.effect0[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
    }
}