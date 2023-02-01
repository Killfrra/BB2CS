#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RicochetAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitEnemies = 1,
                CanHitFriends = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 6, 6, 6, 6, 6, },
            },
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {20, 35, 50, 65, 80};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int targetNum;
            float baseAttackDamage;
            float bonusAD;
            float totalAD;
            float multipliedAD;
            float baseDamage;
            float damageToDeal;
            float counter;
            float damagePercent;
            targetNum = GetCastSpellTargetsHitPlusOne();
            baseAttackDamage = GetBaseAttackDamage(owner);
            bonusAD = GetFlatPhysicalDamageMod(owner);
            totalAD = baseAttackDamage + bonusAD;
            multipliedAD = totalAD * 1;
            baseDamage = this.effect0[level];
            damageToDeal = baseDamage + multipliedAD;
            SpellBuffRemove(attacker, nameof(Buffs.Ricochet), attacker, 0);
            if(targetNum == 1)
            {
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            }
            else
            {
                counter = 1;
                damagePercent = 1;
                while(counter < targetNum)
                {
                    damagePercent *= 0.8f;
                    counter++;
                }
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, damagePercent, 0, 0, false, false, attacker);
            }
        }
    }
}