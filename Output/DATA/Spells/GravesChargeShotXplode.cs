#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GravesChargeShotXplode : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {140, 250, 360};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDmg;
            float totalAD;
            float baseAD;
            float bonusAD;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.GravesChargeShotShot)) == 0)
            {
                baseDmg = this.effect0[level];
                totalAD = GetTotalAttackDamage(attacker);
                baseAD = GetBaseAttackDamage(attacker);
                bonusAD = totalAD - baseAD;
                bonusAD *= 1.2f;
                baseDmg += bonusAD;
                ApplyDamage(attacker, target, baseDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
        }
    }
}