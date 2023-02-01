#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BreathstealerSpell : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "head", },
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", "ItemBreathStealer_buf.troy", },
            BuffName = "Breathstealer",
            BuffTextureName = "3049_Prismatic_Sphere.dds",
        };
        float abilityPowerMod;
        float baseDamageMod;
        float bonusDamageMod;
        public BreathstealerSpell(float abilityPowerMod = default, float baseDamageMod = default, float bonusDamageMod = default)
        {
            this.abilityPowerMod = abilityPowerMod;
            this.baseDamageMod = baseDamageMod;
            this.bonusDamageMod = bonusDamageMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.abilityPowerMod);
            //RequireVar(this.physicalDamageMod);
        }
        public override void OnUpdateStats()
        {
            float abilityPowerMod;
            float baseDamageMod;
            float bonusDamageMod;
            abilityPowerMod = this.abilityPowerMod;
            baseDamageMod = this.baseDamageMod;
            bonusDamageMod = this.bonusDamageMod;
            IncFlatMagicDamageMod(owner, abilityPowerMod);
            IncFlatPhysicalDamageMod(owner, bonusDamageMod);
            IncFlatPhysicalDamageMod(owner, baseDamageMod);
        }
    }
}
namespace Spells
{
    public class BreathstealerSpell : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float debuffMod;
            float currentAbilityPower;
            float currentBonusDamage;
            float currentBaseDamage;
            float abilityPowerMod;
            float bonusDamageMod;
            float baseDamageMod;
            float nextBuffVars_AbilityPowerMod;
            float nextBuffVars_BonusDamageMod;
            float nextBuffVars_BaseDamageMod;
            debuffMod = -0.7f;
            currentAbilityPower = GetFlatMagicDamageMod(target);
            currentBonusDamage = GetFlatPhysicalDamageMod(target);
            currentBaseDamage = GetBaseAttackDamage(target);
            abilityPowerMod = debuffMod * currentAbilityPower;
            bonusDamageMod = debuffMod * currentBonusDamage;
            baseDamageMod = debuffMod * currentBaseDamage;
            nextBuffVars_AbilityPowerMod = abilityPowerMod;
            nextBuffVars_BonusDamageMod = bonusDamageMod;
            nextBuffVars_BaseDamageMod = baseDamageMod;
            AddBuff((ObjAIBase)owner, target, new Buffs.BreathstealerSpell(nextBuffVars_AbilityPowerMod, nextBuffVars_BaseDamageMod, nextBuffVars_BonusDamageMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
        }
    }
}