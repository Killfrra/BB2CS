#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonNoxianDiplomacy : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "TalonNoxianDiplomacy",
            BuffTextureName = "Armsmaster_Empower.dds",
            IsDeathRecapSource = true,
        };
        float bonusDamage;
        int[] effect0 = {8, 7, 6, 5, 4};
        int[] effect1 = {30, 60, 90, 120, 150};
        public override void OnActivate()
        {
            float nextBuffVars_MoveSpeedMod;
            float totalAD;
            float baseAD;
            float bonusAD;
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
            SetDodgePiercing(owner, true);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TalonNoxianDiplomacyBuff)) == 0)
            {
            }
            nextBuffVars_MoveSpeedMod = 0.3f;
            totalAD = GetTotalAttackDamage(attacker);
            baseAD = GetBaseAttackDamage(attacker);
            bonusAD = totalAD - baseAD;
            this.bonusDamage = bonusAD * 0.3f;
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float cooldownVal;
            float flatCDVal;
            float flatCD;
            RemoveOverrideAutoAttack(owner, true);
            SetDodgePiercing(owner, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TalonNoxianDiplomacyBufff)) > 0)
            {
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldownVal = this.effect0[level];
            flatCDVal = 0;
            flatCD = GetPercentCooldownMod(owner);
            flatCDVal = cooldownVal * flatCD;
            cooldownVal += flatCDVal;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetSlotSpellCooldownTimeVer2(cooldownVal, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnUpdateStats()
        {
            int nextBuffVars_MoveSpeedMod;
            nextBuffVars_MoveSpeedMod = 0;
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int nextBuffVars_MoveSpeedMod;
            int nextBuffVars_MissChance;
            int totalIncValue; // UNUSED
            int level;
            float coreDamage;
            nextBuffVars_MoveSpeedMod = 0;
            totalIncValue = 0;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            coreDamage = this.effect1[level];
            damageAmount += coreDamage;
            damageAmount += this.bonusDamage;
            ApplyDamage(attacker, target, 0, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 0, 0, 0, false, false, attacker);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.TalonNoxianDiplomacyBuff)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.TalonNoxianDiplomacyBuff));
                SpellBuffClear(owner, nameof(Buffs.TalonNoxianDiplomacy));
                nextBuffVars_MissChance = 1;
            }
            if(target is Champion)
            {
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.TalonBleedDebuff(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Spells
{
    public class TalonNoxianDiplomacy : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.TalonNoxianDiplomacyBuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
            AddBuff(attacker, target, new Buffs.TalonNoxianDiplomacy(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            CancelAutoAttack(owner, true);
        }
    }
}