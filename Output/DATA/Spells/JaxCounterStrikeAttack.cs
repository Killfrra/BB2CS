#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JaxCounterStrikeAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {40, 70, 100, 130, 160};
        public override void SelfExecute()
        {
            Particle addPart; // UNUSED
            PlayAnimation("Spell3B", 0, attacker, false, false, false);
            SpellEffectCreate(out addPart, out _, "Counterstrike_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDmg;
            float totalAD;
            float baseAD;
            float bonusAD;
            float damage;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDmg = this.effect0[level];
            totalAD = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            bonusAD = totalAD - baseAD;
            bonusAD *= 0.8f;
            damage = bonusAD + baseDmg;
            damage += charVars.NumCounter;
            ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            ApplyStun(attacker, target, 1);
        }
    }
}