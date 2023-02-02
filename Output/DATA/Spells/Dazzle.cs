#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Dazzle : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 12f, 11f, 10f, 9f, 8f, },
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {40, 70, 100, 130, 160};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 ownerPos;
            Vector3 targetPos;
            float distance;
            float aPStat;
            float baseDamage;
            float maxMultiplier;
            float dazzleDamage;
            float castRange;
            float fullDamageRange;
            float varyingRange;
            ownerPos = GetUnitPosition(owner);
            targetPos = GetUnitPosition(target);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            aPStat = GetFlatMagicDamageMod(owner);
            aPStat *= 0.4f;
            baseDamage = this.effect0[level];
            maxMultiplier = 2;
            maxMultiplier--;
            dazzleDamage = baseDamage + aPStat;
            castRange = GetCastRange((ObjAIBase)owner, 2, SpellSlotType.SpellSlots);
            fullDamageRange = 250;
            varyingRange = castRange - fullDamageRange;
            if(distance < castRange)
            {
                float multiplier;
                distance -= fullDamageRange;
                multiplier = distance / varyingRange;
                multiplier = 1 - multiplier;
                if(multiplier > 1)
                {
                    multiplier = 1;
                }
                multiplier *= maxMultiplier;
                multiplier++;
                dazzleDamage *= multiplier;
            }
            ApplyStun(attacker, target, 1.5f);
            ApplyDamage(attacker, target, dazzleDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
        }
    }
}