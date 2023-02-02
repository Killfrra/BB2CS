#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AkaliShadowSwipe : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {30, 55, 80, 105, 130};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float bonusDamage;
            float akaliDamage;
            float akaliAP;
            float damageToDeal;
            bool isStealthed;
            bonusDamage = this.effect0[level];
            akaliDamage = GetTotalAttackDamage(owner);
            akaliAP = GetFlatMagicDamageMod(owner);
            akaliAP *= 0.3f;
            akaliDamage *= 0.6f;
            damageToDeal = bonusDamage + akaliDamage;
            damageToDeal += akaliAP;
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                ApplyDamage((ObjAIBase)owner, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, true, false, attacker);
            }
            else if(target is Champion)
            {
                ApplyDamage((ObjAIBase)owner, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, true, false, attacker);
            }
            else
            {
                bool canSee;
                canSee = CanSeeTarget(owner, target);
                if(canSee)
                {
                    ApplyDamage((ObjAIBase)owner, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, true, false, attacker);
                }
            }
        }
    }
}