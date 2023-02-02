#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GGSpray : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {-1, -2, -3, -4, -5};
        int[] effect1 = {10, 16, 22, 28, 34};
        int[] effect2 = {10, 16, 22, 28, 34};
        int[] effect3 = {10, 16, 22, 28, 34};
        int[] effect4 = {10, 16, 22, 28, 34};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_ArmorMod;
            int baseDamage; // UNUSED
            float totalDamage;
            float baseAD;
            float bonusDamage;
            bool isStealthed;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_ArmorMod = this.effect0[level];
            baseDamage = this.effect1[level];
            totalDamage = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            bonusDamage = totalDamage - baseAD;
            bonusDamage *= 0.2f;
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                AddBuff(attacker, target, new Buffs.GatlingDebuff(nextBuffVars_ArmorMod), 10, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                ApplyDamage((ObjAIBase)owner, target, bonusDamage + this.effect2[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else
            {
                if(target is Champion)
                {
                    AddBuff(attacker, target, new Buffs.GatlingDebuff(nextBuffVars_ArmorMod), 10, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                    ApplyDamage((ObjAIBase)owner, target, bonusDamage + this.effect3[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                }
                else
                {
                    bool canSee;
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        AddBuff(attacker, target, new Buffs.GatlingDebuff(nextBuffVars_ArmorMod), 10, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                        ApplyDamage((ObjAIBase)owner, target, bonusDamage + this.effect4[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                    }
                }
            }
        }
    }
}