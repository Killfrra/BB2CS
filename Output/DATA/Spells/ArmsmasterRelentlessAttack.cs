#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ArmsmasterRelentlessAttack : BBSpellScript
    {
        int[] effect0 = {60, 95, 130, 165, 200};
        int[] effect1 = {140, 170, 210};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    float attackDamage; // UNUSED
                    float baseAttackDamage;
                    Particle a; // UNUSED
                    attackDamage = GetTotalAttackDamage(owner);
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.EmpowerTwo)) > 0)
                    {
                        float bonusAttackDamage;
                        float bonusDamage;
                        float physicalBonus;
                        float aOEDmg;
                        bonusAttackDamage = GetFlatPhysicalDamageMod(owner);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        bonusDamage = this.effect0[level];
                        physicalBonus = bonusAttackDamage * 0.4f;
                        aOEDmg = physicalBonus + bonusDamage;
                        BreakSpellShields(target);
                        ApplyDamage(attacker, target, aOEDmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                        SpellBuffRemove(owner, nameof(Buffs.EmpowerTwo), (ObjAIBase)owner, 0);
                    }
                    baseAttackDamage = GetBaseAttackDamage(owner);
                    ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage((ObjAIBase)owner, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 1, false, false, attacker);
                    SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.RelentlessAssaultDebuff), 0);
                    SpellEffectCreate(out a, out _, "RelentlessAssault_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
            }
        }
    }
}