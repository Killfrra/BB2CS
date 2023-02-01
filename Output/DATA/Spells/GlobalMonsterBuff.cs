#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GlobalMonsterBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        float spawnTime;
        float healthPerMinute;
        float damagePerMinute;
        float goldPerMinute;
        float expPerMinute;
        bool upgradeTimer;
        float lastTimeExecuted;
        public GlobalMonsterBuff(float spawnTime = default, float healthPerMinute = default, float damagePerMinute = default, float goldPerMinute = default, float expPerMinute = default, bool upgradeTimer = default)
        {
            this.spawnTime = spawnTime;
            this.healthPerMinute = healthPerMinute;
            this.damagePerMinute = damagePerMinute;
            this.goldPerMinute = goldPerMinute;
            this.expPerMinute = expPerMinute;
            this.upgradeTimer = upgradeTimer;
        }
        public override void OnActivate()
        {
            float gameTime;
            float jungleLifeTime;
            float bonusHealth;
            float bonusDamage;
            float bonusGold;
            float bonusExp;
            //RequireVar(this.spawnTime);
            //RequireVar(this.healthPerMinute);
            //RequireVar(this.damagePerMinute);
            //RequireVar(this.goldPerMinute);
            //RequireVar(this.expPerMinute);
            //RequireVar(this.upgradeTimer);
            gameTime = GetGameTime();
            jungleLifeTime = gameTime - this.spawnTime;
            jungleLifeTime = Math.Max(jungleLifeTime, 0);
            bonusHealth = jungleLifeTime * this.healthPerMinute;
            bonusHealth /= 60;
            bonusDamage = jungleLifeTime * this.damagePerMinute;
            bonusDamage /= 60;
            bonusGold = jungleLifeTime * this.goldPerMinute;
            bonusGold /= 60;
            bonusExp = jungleLifeTime * this.expPerMinute;
            bonusExp /= 60;
            IncPermanentFlatHPPoolMod(owner, bonusHealth);
            IncPermanentFlatPhysicalDamageMod(owner, bonusDamage);
            IncPermanentExpReward(owner, bonusExp);
            IncPermanentGoldReward(owner, bonusGold);
        }
        public override void OnUpdateStats()
        {
            float healthPercent;
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if(healthPercent >= 0.995f)
            {
                if(this.upgradeTimer)
                {
                    if(ExecutePeriodically(60, ref this.lastTimeExecuted, false))
                    {
                        IncPermanentFlatHPPoolMod(owner, this.healthPerMinute);
                        IncPermanentFlatPhysicalDamageMod(owner, this.damagePerMinute);
                        IncPermanentExpReward(owner, this.expPerMinute);
                        IncPermanentGoldReward(owner, this.goldPerMinute);
                    }
                }
            }
        }
    }
}