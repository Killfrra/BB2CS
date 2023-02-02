#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RivenLightsaberMissileSide : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 120, 160, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.RivenLightsaberMissileDebuff)) == 0)
            {
                float healthPercent;
                float bonusRatio;
                float multiplier;
                AddBuff(attacker, target, new Buffs.RivenLightsaberMissileDebuff(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                healthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                bonusRatio = 1 - healthPercent;
                bonusRatio /= 0.75f;
                bonusRatio = Math.Min(bonusRatio, 1);
                bonusRatio *= 2;
                multiplier = 1 + bonusRatio;
                ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, multiplier, 0, 0.6f, false, false, attacker);
                level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            }
        }
    }
}