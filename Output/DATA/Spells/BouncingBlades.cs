#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BouncingBlades : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 2, 3, 4, 5, 6, },
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 95, 130, 165, 200};
        int[] effect1 = {8, 12, 16, 20, 24};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float bBBaseDamage;
            float totalDamage;
            float baseDamage;
            float bonusDamage;
            float bbBonusDamage;
            float damageVar;
            float kIDamage;
            int bBCounter;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bBBaseDamage = this.effect0[level];
            totalDamage = GetTotalAttackDamage(owner);
            baseDamage = GetBaseAttackDamage(owner);
            bonusDamage = totalDamage - baseDamage;
            bbBonusDamage = bonusDamage * 0.8f;
            damageVar = bbBonusDamage + bBBaseDamage;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            kIDamage = this.effect1[level];
            damageVar += kIDamage;
            bBCounter = GetCastSpellTargetsHitPlusOne();
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KillerInstinct)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KillerInstinct), (ObjAIBase)owner);
                AddBuff(attacker, owner, new Buffs.KillerInstinctBuff2(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KillerInstinctBuff2)) > 0)
            {
                int targetNum; // UNUSED
                AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                targetNum = GetCastSpellTargetsHitPlusOne();
                ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false, attacker);
            }
            else
            {
                float bBCount;
                float inverseVar;
                float percentVar;
                bBCount = bBCounter - 1;
                inverseVar = bBCount * 0.1f;
                percentVar = 1 - inverseVar;
                ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentVar, 0.35f, 1, false, false, attacker);
            }
        }
    }
}