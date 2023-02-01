#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LuxPrismaticWave : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {80, 105, 130, 155, 180};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            float baseDamageBlock;
            float abilityPower;
            float bonusHealth;
            float damageBlock;
            float nextBuffVars_DamageBlock;
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 1000)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 950, 0);
            }
            baseDamageBlock = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(owner);
            bonusHealth = abilityPower * 0.35f;
            damageBlock = baseDamageBlock + bonusHealth;
            nextBuffVars_DamageBlock = damageBlock;
            AddBuff(attacker, target, new Buffs.LuxPrismaticWaveShieldSelf(nextBuffVars_DamageBlock), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
        }
    }
}