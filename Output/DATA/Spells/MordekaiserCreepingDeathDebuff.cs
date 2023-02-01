#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserCreepingDeathDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserCreepingDeathDebuff",
            BuffTextureName = "FallenAngel_TormentedSoil.dds",
        };
        float damagePerTick;
        int count;
        float[] effect0 = {0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f};
        float[] effect1 = {0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f};
        public MordekaiserCreepingDeathDebuff(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            this.count = 0;
            ApplyDamage((ObjAIBase)owner, attacker, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            int level;
            float percentLeech;
            float shieldAmount;
            if(this.count == 0)
            {
                level = GetLevel(owner);
                if(target is Champion)
                {
                    percentLeech = this.effect0[level];
                }
                else
                {
                    percentLeech = this.effect1[level];
                }
                shieldAmount = percentLeech * damageAmount;
                IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                this.count = 1;
            }
        }
    }
}