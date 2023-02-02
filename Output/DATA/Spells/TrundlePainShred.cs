#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundlePainShred : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundlePainShred",
            BuffTextureName = "Trundle_Agony.dds",
        };
        float survivability;
        float ownerArmor;
        float ownerMR;
        float lowerArmor;
        float lowerMR;
        float instancedArmor;
        float instancedMR;
        float lastTimeExecuted;
        public TrundlePainShred(float survivability = default)
        {
            this.survivability = survivability;
        }
        public override void OnActivate()
        {
            //RequireVar(this.survivability);
            this.ownerArmor = GetArmor(owner);
            this.ownerMR = GetSpellBlock(owner);
            this.lowerArmor = this.survivability * this.ownerArmor;
            this.lowerMR = this.survivability * this.ownerMR;
            this.survivability /= 6;
            this.instancedArmor = this.lowerArmor * -1;
            this.instancedMR = this.lowerMR * -1;
            if(this.instancedArmor < 0)
            {
                IncFlatArmorMod(owner, this.instancedArmor);
            }
            IncFlatArmorMod(owner, this.instancedArmor);
            if(this.instancedMR < 0)
            {
                IncFlatSpellBlockMod(owner, this.instancedMR);
            }
            IncFlatSpellBlockMod(owner, this.instancedMR);
            AddBuff(attacker, attacker, new Buffs.TrundlePainBuff(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            float trundleArmor;
            float trundleMR;
            if(this.instancedArmor < 0)
            {
                IncFlatArmorMod(owner, this.instancedArmor);
            }
            if(this.instancedMR < 0)
            {
                IncFlatSpellBlockMod(owner, this.instancedMR);
            }
            trundleArmor = this.instancedArmor * -1;
            trundleMR = this.instancedMR * -1;
            if(trundleArmor > 0)
            {
                IncFlatArmorMod(attacker, trundleArmor);
            }
            if(trundleMR > 0)
            {
                IncFlatSpellBlockMod(attacker, trundleMR);
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                float lowerArmorLess;
                float lowerMRLess;
                lowerArmorLess = this.survivability * this.ownerArmor;
                lowerMRLess = this.survivability * this.ownerMR;
                this.instancedArmor -= lowerArmorLess;
                this.instancedMR -= lowerMRLess;
            }
        }
    }
}