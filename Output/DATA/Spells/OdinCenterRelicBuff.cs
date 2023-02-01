#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinCenterRelicBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "PotionofBrilliance_itm.troy", "", },
            BuffName = "OdinCenterRelic",
            BuffTextureName = "StormShield.dds",
            NonDispellable = true,
        };
        float totalArmorAmount;
        Particle buffParticle2;
        float lastTimeExecuted;
        float oldArmorAmount;
        public override void OnActivate()
        {
            int level;
            float bonusShieldHP;
            level = GetLevel(owner);
            bonusShieldHP = level * 25;
            this.totalArmorAmount = bonusShieldHP + 100;
            IncreaseShield(owner, this.totalArmorAmount, true, true);
            SetBuffToolTipVar(1, this.totalArmorAmount);
            SpellEffectCreate(out this.buffParticle2, out _, "odin_center_relic.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCenterRelicShieldCheck2(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveShield(owner, this.totalArmorAmount, true, true);
            SpellEffectRemove(this.buffParticle2);
        }
        public override void OnUpdateActions()
        {
            int level;
            float bonusShieldHP;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCenterRelicShieldCheck)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinCenterRelicShieldCheck2)) == 0)
                    {
                        RemoveShield(owner, this.totalArmorAmount, true, true);
                        level = GetLevel(owner);
                        bonusShieldHP = level * 25;
                        this.totalArmorAmount = bonusShieldHP + 100;
                        IncreaseShield(owner, this.totalArmorAmount, true, true);
                        SetBuffToolTipVar(1, this.totalArmorAmount);
                        SpellEffectRemove(this.buffParticle2);
                        SpellEffectCreate(out this.buffParticle2, out _, "odin_center_relic.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                        AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCenterRelicShieldCheck2(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCenterRelicShieldCheck(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            this.oldArmorAmount = this.totalArmorAmount;
            if(this.totalArmorAmount >= damageAmount)
            {
                this.totalArmorAmount -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.totalArmorAmount;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.totalArmorAmount;
                this.totalArmorAmount = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                RemoveShield(owner, this.totalArmorAmount, true, true);
                SpellEffectRemove(this.buffParticle2);
            }
            SetBuffToolTipVar(1, this.totalArmorAmount);
        }
    }
}