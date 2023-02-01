#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickGhoulCounter2 : BBBuffScript
    {
        public override void OnActivate()
        {
            int yorickLevel;
            float yorickAP;
            float healthFromAP;
            float aDFromAP;
            float healthFromLevel;
            float aDFromLevel;
            float totalAD;
            float totalHealth;
            yorickLevel = GetLevel(owner);
            yorickAP = GetFlatMagicDamageMod(owner);
            healthFromAP = yorickAP * 1;
            aDFromAP = yorickAP * 0.2f;
            healthFromLevel = 50 * yorickLevel;
            aDFromLevel = 2 * yorickLevel;
            totalAD = aDFromLevel + aDFromAP;
            totalHealth = healthFromLevel + healthFromAP;
            IncPermanentFlatHPPoolMod(attacker, totalHealth);
            IncPermanentFlatPhysicalDamageMod(attacker, totalAD);
            IncPermanentFlatArmorMod(attacker, totalAD);
            IncPermanentFlatSpellBlockMod(attacker, totalAD);
        }
        public override void OnDeactivate(bool expired)
        {
            if(!attacker.IsDead)
            {
                ApplyDamage(attacker, attacker, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
            }
        }
    }
}