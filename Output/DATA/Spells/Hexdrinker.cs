#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Hexdrinker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "hexTech_dmg_shield_duration.troy", "", "", },
            BuffName = "HexdrunkEmpowered",
            BuffTextureName = "3155_Hexdrinker.dds",
            OnPreDamagePriority = 2,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldHealth;
        float oldArmorAmount;
        public Hexdrinker(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldHealth);
            IncreaseShield(owner, this.shieldHealth, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float duration;
            duration = 60 - lifeTime;
            AddBuff((ObjAIBase)owner, owner, new Buffs.HexdrinkerTimerCD(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            if(this.shieldHealth > 0)
            {
                RemoveShield(owner, this.shieldHealth, true, false);
            }
        }
        public override void OnUpdateActions()
        {
            if(this.shieldHealth <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            Particle a; // UNUSED
            Particle b; // UNUSED
            this.oldArmorAmount = this.shieldHealth;
            if(damageAmount > 0)
            {
                if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
                {
                    SpellEffectCreate(out a, out _, "hexTech_dmg_shield_onHit_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    SpellEffectCreate(out b, out _, "hexTech_dmg_shield_onHit_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    if(this.shieldHealth >= damageAmount)
                    {
                        this.shieldHealth -= damageAmount;
                        damageAmount = 0;
                        this.oldArmorAmount -= this.shieldHealth;
                        ReduceShield(owner, this.oldArmorAmount, true, false);
                    }
                    else
                    {
                        damageAmount -= this.shieldHealth;
                        this.shieldHealth = 0;
                        ReduceShield(owner, this.oldArmorAmount, true, false);
                    }
                }
            }
        }
    }
}