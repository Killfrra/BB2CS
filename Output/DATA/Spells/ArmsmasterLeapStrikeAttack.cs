#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ArmsmasterLeapStrikeAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 95, 130, 165, 200};
        int[] effect1 = {20, 45, 70, 95, 120};
        public override void SelfExecute()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeapStrikeSpeed(), 1, 1, 0.35f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float bonusAttackDamage;
            float bonusDamage;
            float physicalBonus;
            float aOEDmg;
            float attackDamage;
            float damageToDeal;
            BreakSpellShields(target);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.EmpowerTwo)) > 0)
            {
                bonusAttackDamage = GetFlatPhysicalDamageMod(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                bonusDamage = this.effect0[level];
                physicalBonus = bonusAttackDamage * 0.4f;
                aOEDmg = physicalBonus + bonusDamage;
                ApplyDamage(attacker, target, aOEDmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0.7f, false, false, attacker);
                SpellBuffRemove(owner, nameof(Buffs.EmpowerTwo), (ObjAIBase)owner, 0);
            }
            attackDamage = GetTotalAttackDamage(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusDamage = this.effect1[level];
            damageToDeal = attackDamage + bonusDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 0, false, false, attacker);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RelentlessAssaultMarker)) > 0)
            {
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.RelentlessAssaultMarker), 0);
            }
            if(target is Champion)
            {
                if(owner.Team != target.Team)
                {
                    IssueOrder(owner, OrderType.AttackTo, default, target);
                }
            }
        }
    }
}