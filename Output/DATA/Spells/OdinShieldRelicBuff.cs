#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShieldRelicBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "OdinShieldRelic",
            BuffTextureName = "JarvanIV_GoldenAegis.dds",
            NonDispellable = true,
        };
        Particle particle1;
        Particle buffParticle;
        float totalShield;
        float oldArmorAmount;
        public override void OnActivate()
        {
            int level;
            float baseShield;
            float levelShield;
            SpellEffectCreate(out this.particle1, out _, "regen_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            SpellEffectCreate(out this.buffParticle, out _, "regen_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            IncPercentArmorPenetrationMod(owner, 0.2f);
            IncPercentMagicPenetrationMod(owner, 0.2f);
            level = GetLevel(owner);
            baseShield = 140;
            levelShield = level * 20;
            this.totalShield = levelShield + baseShield;
            SetBuffToolTipVar(1, this.totalShield);
            IncreaseShield(owner, this.totalShield, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
            SpellEffectRemove(this.particle1);
            if(this.totalShield > 0)
            {
                RemoveShield(owner, this.totalShield, true, true);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorPenetrationMod(owner, 0.2f);
            IncPercentMagicPenetrationMod(owner, 0.2f);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.totalShield;
            if(this.totalShield >= damageAmount)
            {
                this.totalShield -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.totalShield;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.totalShield;
                this.totalShield = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
            SetBuffToolTipVar(1, this.totalShield);
        }
    }
}