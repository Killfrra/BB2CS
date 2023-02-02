#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaSunlightPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeonaSunlightPassive",
            BuffTextureName = "LeonaSunlight.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int lastLifesteal; // UNUSED
        float lastSunlightDamage;
        float lastTimeExecuted;
        int[] effect0 = {20, 20, 35, 35, 50, 50, 65, 65, 80, 80, 95, 95, 110, 110, 125, 125, 140, 140};
        public override void OnActivate()
        {
            this.lastLifesteal = 0;
            this.lastSunlightDamage = 0;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                int level;
                float sunlightDamage;
                level = GetLevel(owner);
                sunlightDamage = this.effect0[level];
                if(sunlightDamage > this.lastSunlightDamage)
                {
                    this.lastSunlightDamage = sunlightDamage;
                    SetBuffToolTipVar(2, sunlightDamage);
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeonaSolarBarrierTracker)) > 0)
                    {
                        if(target is ObjAIBase)
                        {
                            if(target is not BaseTurret)
                            {
                                AddBuff(attacker, target, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}