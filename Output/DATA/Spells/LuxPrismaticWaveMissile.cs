#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LuxPrismaticWaveMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {80, 105, 130, 155, 180};
        int[] effect1 = {80, 105, 130, 155, 180};
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            float baseDamageBlock;
            float abilityPower;
            float bonusHealth;
            float damageBlock;
            float nextBuffVars_DamageBlock;
            level = GetCastSpellLevelPlusOne();
            baseDamageBlock = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(attacker);
            bonusHealth = abilityPower * 0.35f;
            damageBlock = baseDamageBlock + bonusHealth;
            nextBuffVars_DamageBlock = damageBlock;
            AddBuff(attacker, attacker, new Buffs.LuxPrismaticWaveShieldSelf(nextBuffVars_DamageBlock), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamageBlock;
            float abilityPower;
            float bonusHealth;
            float damageBlock;
            float nextBuffVars_DamageBlock;
            if(attacker != target)
            {
                baseDamageBlock = this.effect1[level];
                abilityPower = GetFlatMagicDamageMod(owner);
                bonusHealth = abilityPower * 0.35f;
                damageBlock = baseDamageBlock + bonusHealth;
                nextBuffVars_DamageBlock = damageBlock;
                AddBuff(attacker, target, new Buffs.LuxPrismaticWaveShield(nextBuffVars_DamageBlock), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}