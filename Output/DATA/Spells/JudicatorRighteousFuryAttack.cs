#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JudicatorRighteousFuryAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        int[] effect1 = {20, 30, 40, 50, 60};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float tAD;
            float damagePercent;
            float cleaveDamage;
            float baseDamage;
            float abilityPower;
            float bonusDamage;
            float damageToApply;
            float damageToApplySlash;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            tAD = GetTotalAttackDamage(owner);
            damagePercent = this.effect0[level];
            cleaveDamage = tAD * damagePercent;
            baseDamage = GetBaseAttackDamage(owner);
            abilityPower = GetFlatMagicDamageMod(owner);
            bonusDamage = this.effect1[level];
            abilityPower *= 0.2f;
            damageToApply = bonusDamage + abilityPower;
            damageToApplySlash = cleaveDamage + damageToApply;
            ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, default, true))
            {
                if(target is not BaseTurret)
                {
                    if(unit != target)
                    {
                        ApplyDamage((ObjAIBase)owner, unit, damageToApplySlash, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    }
                    else
                    {
                        ApplyDamage((ObjAIBase)owner, unit, damageToApply, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}