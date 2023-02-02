#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShenWayOfTheNinjaMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shen Passive Marker",
            BuffTextureName = "Shen_KiStrike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float shurikenDamage;
        float lastHit;
        int[] effect0 = {10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105};
        public override void OnActivate()
        {
            this.shurikenDamage = 10;
            SetBuffToolTipVar(1, this.shurikenDamage);
            this.lastHit = 0;
        }
        public override void OnUpdateActions()
        {
            int level;
            float shurikenDamage;
            float maxHP;
            float bonusDmgFromHP;
            float finalDamage;
            level = GetLevel(owner);
            shurikenDamage = this.effect0[level];
            maxHP = GetFlatHPPoolMod(owner);
            bonusDmgFromHP = maxHP * 0.08f;
            finalDamage = bonusDmgFromHP + shurikenDamage;
            SetBuffToolTipVar(1, shurikenDamage);
            SetBuffToolTipVar(2, finalDamage);
            SetBuffToolTipVar(3, bonusDmgFromHP);
            if(!owner.IsDead)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShenWayOfTheNinjaAura)) == 0)
                {
                    float curTime;
                    float timeSinceLastHit;
                    curTime = GetGameTime();
                    timeSinceLastHit = curTime - this.lastHit;
                    if(timeSinceLastHit >= 8)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShenWayOfTheNinjaAura)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.ShenWayOfTheNinjaAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Miss)
            {
                if(hitResult != HitResult.HIT_Dodge)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShenWayOfTheNinjaAura)) > 0)
                    {
                        this.lastHit = GetGameTime();
                    }
                }
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(attacker is Champion)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(hitResult != HitResult.HIT_Dodge)
                    {
                        float curTime;
                        float timeSinceLastHit;
                        this.lastHit -= 2;
                        curTime = GetGameTime();
                        timeSinceLastHit = curTime - this.lastHit;
                        if(timeSinceLastHit >= 8)
                        {
                            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShenWayOfTheNinjaAura)) == 0)
                            {
                                AddBuff((ObjAIBase)owner, owner, new Buffs.ShenWayOfTheNinjaAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}