#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MissFortuneBulletTimeCast : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {6, 9, 12, 15, 18};
        float[] effect1 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDmg;
            float dmgPerLvl;
            float perLevel;
            float multiDmg;
            float finalDmg;
            int count; // UNUSED
            baseDmg = GetTotalAttackDamage(owner);
            dmgPerLvl = this.effect0[level];
            perLevel = this.effect1[level];
            multiDmg = baseDmg * perLevel;
            finalDmg = multiDmg + dmgPerLvl;
            if(target is Champion)
            {
                finalDmg *= 2;
                ApplyDamage(attacker, target, finalDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else
            {
                ApplyDamage(attacker, target, finalDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            count = GetBuffCountFromAll(target);
        }
    }
}