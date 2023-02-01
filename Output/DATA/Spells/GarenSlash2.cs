#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GarenSlash2 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {2.5f, 2.5f, 2.5f, 2.5f, 2.5f};
        int[] effect1 = {30, 45, 60, 75, 90};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float silenceDuration;
            float bonusDamage;
            float supremeDmg;
            float scalingDamage;
            float dealtDamage;
            silenceDuration = this.effect0[level];
            bonusDamage = this.effect1[level];
            supremeDmg = GetTotalAttackDamage(owner);
            scalingDamage = supremeDmg * 1.4f;
            dealtDamage = scalingDamage + bonusDamage;
            hitResult = false;
            if(target is ObjAIBase)
            {
                BreakSpellShields(target);
                ApplyDamage(attacker, target, dealtDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
                if(target is not BaseTurret)
                {
                    ApplySilence(attacker, target, silenceDuration);
                }
            }
            else
            {
                ApplyDamage(attacker, target, dealtDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            }
        }
    }
}