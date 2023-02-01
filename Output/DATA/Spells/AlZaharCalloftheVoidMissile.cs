#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AlZaharCalloftheVoidMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {40, 67.5f, 95, 122.5f, 150};
        float[] effect1 = {1.4f, 1.8f, 2.2f, 2.6f, 3};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            float abilityPower;
            float abilityPowerMod;
            float totalDamage;
            float silenceDuration;
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(attacker);
            abilityPowerMod = abilityPower * 0.4f;
            totalDamage = abilityPowerMod + baseDamage;
            silenceDuration = this.effect1[level];
            ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false);
            if(target is Champion)
            {
                AddBuff(attacker, target, new Buffs.Silence(), 1, 1, silenceDuration, BuffAddType.RENEW_EXISTING, BuffType.SILENCE, 0, true);
            }
        }
    }
}