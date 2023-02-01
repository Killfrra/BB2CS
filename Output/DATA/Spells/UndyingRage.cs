#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UndyingRage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", "", "", "spine", },
            AutoBuffActivateEffect = new[]{ "UndyingRage_buf.troy", "", "", "UndyingRageSpine_glow.troy", },
            BuffName = "Undying Rage",
            BuffTextureName = "DarkChampion_EndlessRage.dds",
            NonDispellable = true,
            OnPreDamagePriority = 2,
            SpellFXOverrideSkins = new[]{ "TryndamereDemonsword", },
            SpellVOOverrideSkins = new[]{ "TryndamereDemonsword", },
        };
        Particle a;
        Particle b;
        public override void OnActivate()
        {
            OverrideAnimation("run", "run2", owner);
            SpellEffectCreate(out this.a, out _, "UndyingRage_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.b, out _, "UndyingRage_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float healthPercent;
            float health;
            float maxHealth;
            float healthFactor;
            float healthToInc;
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if(healthPercent <= 0.03f)
            {
                health = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                healthFactor = maxHealth * 0.03f;
                healthToInc = healthFactor - health;
                IncHealth(owner, healthToInc, owner);
            }
            ClearOverrideAnimation("Run", owner);
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.b);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth;
            curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(curHealth <= damageAmount)
            {
                damageAmount = curHealth - 1;
                Say(owner, "game_lua_UndyingRage");
            }
        }
    }
}
namespace Spells
{
    public class UndyingRage : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 110f, 100f, 90f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {50, 75, 100};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.UndyingRage(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            IncPAR(owner, this.effect0[level], PrimaryAbilityResourceType.Other);
        }
    }
}