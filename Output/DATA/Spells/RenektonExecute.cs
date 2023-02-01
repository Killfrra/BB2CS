#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RenektonExecute : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            IsDamagingSpell = true,
        };
        int[] effect0 = {5, 15, 25, 35, 45};
        float[] effect1 = {0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f};
        int[] effect2 = {1, 1, 1, 1, 1};
        int[] effect3 = {1, 1, 1, 1, 1};
        int[] effect4 = {1, 1, 1, 1, 1};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float ragePercent;
            bool furyBonus;
            float bonusDamage;
            float currentFury;
            float baseDamage;
            float bonusPercent;
            float postFury;
            float furyCost;
            if(hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if(hitResult == HitResult.HIT_Miss)
            {
                hitResult = HitResult.HIT_Normal;
            }
            ragePercent = GetPARPercent(owner, PrimaryAbilityResourceType.Other);
            furyBonus = false;
            bonusDamage = this.effect0[level];
            if(ragePercent >= 0.5f)
            {
                furyBonus = true;
                IncPAR(owner, -50, PrimaryAbilityResourceType.Other);
                currentFury = GetPAR(owner, PrimaryAbilityResourceType.Other);
            }
            BreakSpellShields(target);
            if(furyBonus)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonUnlockAnimation(), 1, 1, 0.51f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonUnlockAnimation(), 1, 1, 0.3f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            baseDamage = GetBaseAttackDamage(owner);
            bonusPercent = this.effect1[level];
            baseDamage *= bonusPercent;
            baseDamage += bonusDamage;
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, this.effect2[level], 0, bonusPercent, false, true, attacker);
            if(target is ObjAIBase)
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, this.effect3[level], 0, bonusPercent, false, true, attacker);
                if(!furyBonus)
                {
                    ApplyStun(attacker, target, 0.75f);
                }
            }
            if(furyBonus)
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, this.effect4[level], 0, bonusPercent, false, true, attacker);
                SpellBuffClear(owner, nameof(Buffs.RenektonRageReady));
            }
            if(furyBonus)
            {
                postFury = GetPAR(owner, PrimaryAbilityResourceType.Other);
                furyCost = currentFury - postFury;
                IncPAR(owner, furyCost, PrimaryAbilityResourceType.Other);
                ApplyStun(attacker, target, 1.5f);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonWeaponGlowFade(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetDodgePiercing(owner, false);
        }
    }
}