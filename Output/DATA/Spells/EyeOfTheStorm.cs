#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class EyeOfTheStorm : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 120, 160, 200, 240};
        int[] effect1 = {14, 23, 32, 41, 50};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float abilityPower;
            float armorAmount;
            float totalArmorAmount;
            float nextBuffVars_TotalArmorAmount;
            int nextBuffVars_DamageBonus;
            PlayAnimation("Spell3", 0, owner, false, false, false);
            abilityPower = GetFlatMagicDamageMod(attacker);
            armorAmount = this.effect0[level];
            abilityPower *= 0.9f;
            totalArmorAmount = abilityPower + armorAmount;
            nextBuffVars_TotalArmorAmount = totalArmorAmount;
            nextBuffVars_DamageBonus = this.effect1[level];
            AddBuff(attacker, target, new Buffs.EyeOfTheStorm(nextBuffVars_TotalArmorAmount), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.JannaEoTSBuff(nextBuffVars_DamageBonus), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class EyeOfTheStorm : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.troy",
            BuffName = "Eye Of The Storm",
            BuffTextureName = "Janna_EyeOfTheStorm.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float totalArmorAmount;
        Particle particle;
        float oldArmorAmount;
        public EyeOfTheStorm(float totalArmorAmount = default)
        {
            this.totalArmorAmount = totalArmorAmount;
        }
        public override void OnActivate()
        {
            int attackerSkinID;
            //RequireVar(this.totalArmorAmount);
            SetBuffToolTipVar(1, this.totalArmorAmount);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, this.totalArmorAmount, true, true);
            attackerSkinID = GetSkinID(attacker);
            if(attackerSkinID == 3)
            {
                SpellEffectCreate(out this.particle, out _, "EyeoftheStorm_Frost_Ally_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "EyeoftheStorm_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            if(this.totalArmorAmount > 0)
            {
                RemoveShield(owner, this.totalArmorAmount, true, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
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
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}