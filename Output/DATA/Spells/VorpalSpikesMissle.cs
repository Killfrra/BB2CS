#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VorpalSpikesMissle : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {20, 35, 50, 65, 80};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.VorpalSpikesMissleBuff)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.VorpalSpikesMissleBuff), (ObjAIBase)owner);
            }
            else
            {
                ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 1, false, false, attacker);
            }
        }
    }
}