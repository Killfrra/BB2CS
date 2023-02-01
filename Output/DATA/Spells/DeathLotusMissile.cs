#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class DeathLotusMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {50, 65, 80};
        int[] effect1 = {8, 12, 16, 20, 24};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float daggerBase;
            float kIDamage;
            float totalDamage;
            float baseDamage;
            float bonusDamage;
            float dlBonusDamage;
            float damageToDeal;
            daggerBase = this.effect0[level];
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            kIDamage = this.effect1[level];
            totalDamage = GetTotalAttackDamage(owner);
            baseDamage = GetBaseAttackDamage(owner);
            bonusDamage = totalDamage - baseDamage;
            dlBonusDamage = bonusDamage * 0.5f;
            damageToDeal = dlBonusDamage + daggerBase;
            damageToDeal += kIDamage;
            ApplyDamage((ObjAIBase)owner, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
        }
    }
}