#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickUnholySymbiosis : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "YorickUnholySymbiosis",
            BuffTextureName = "YorickUnholyCovenant.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(2, ref this.lastTimeExecuted, true))
            {
                float yorickAP;
                float aDFromAP;
                float healthFromAP;
                yorickAP = GetFlatMagicDamageMod(owner);
                aDFromAP = yorickAP * 0.2f;
                healthFromAP = yorickAP * 0.5f;
                SetBuffToolTipVar(1, aDFromAP);
                SetBuffToolTipVar(2, healthFromAP);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float passiveMultiplier;
            float count;
            passiveMultiplier = 0.05f;
            count = 0;
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonSpectral)) > 0)
            {
                count++;
            }
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonRavenous)) > 0)
            {
                count++;
            }
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonDecayed)) > 0)
            {
                count++;
            }
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRARemovePet)) > 0)
            {
                count++;
            }
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickUltPetActive)) > 0)
            {
                count++;
            }
            passiveMultiplier *= count;
            passiveMultiplier++;
            damageAmount *= passiveMultiplier;
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float passiveMultiplier;
            float count;
            passiveMultiplier = 0.05f;
            count = 0;
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonSpectral)) > 0)
            {
                count++;
            }
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonRavenous)) > 0)
            {
                count++;
            }
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonDecayed)) > 0)
            {
                count++;
            }
            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRARemovePet)) > 0)
            {
                count++;
            }
            passiveMultiplier *= count;
            passiveMultiplier = 1 - passiveMultiplier;
            damageAmount *= passiveMultiplier;
        }
    }
}