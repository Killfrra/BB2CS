#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class NidaleeTakedownAttack : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            float weaponDamage;
            float damage;
            float takedownDamage;
            float healthPercent;
            float bonusPercent;
            baseDamage = GetBaseAttackDamage(owner);
            weaponDamage = GetFlatPhysicalDamageMod(owner);
            damage = baseDamage + weaponDamage;
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                    ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, default, false, false);
                }
                else
                {
                    takedownDamage = charVars.TakedownDamage;
                    damage += takedownDamage;
                    healthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                    bonusPercent = 1 - healthPercent;
                    bonusPercent *= 2;
                    bonusPercent++;
                    damage *= bonusPercent;
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, default, true, true);
                    SpellEffectCreate(out _, out _, "nidalee_cougar_takedown_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
                }
            }
            else
            {
                ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, default, false, false);
            }
            SpellBuffRemove(owner, nameof(Buffs.Takedown), (ObjAIBase)owner);
        }
    }
}