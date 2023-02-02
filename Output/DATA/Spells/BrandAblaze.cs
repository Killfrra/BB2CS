#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BrandAblaze : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "BrandAblaze",
            BuffTextureName = "BrandBlaze.dds",
            IsDeathRecapSource = true,
        };
        Particle a;
        Particle b;
        Particle c;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            int brandSkinID;
            TeamId teamID;
            brandSkinID = GetSkinID(attacker);
            teamID = GetTeamID(attacker);
            if(brandSkinID == 3)
            {
                SpellEffectCreate(out this.a, out _, "BrandBlaze_hotfoot_Frost.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.b, out _, "BrandBlaze_hotfoot_Frost.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.c, out _, "BrandFireMark_Frost.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.a, out _, "BrandBlaze_hotfoot.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.b, out _, "BrandBlaze_hotfoot.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.c, out _, "BrandFireMark.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            float maxHealth;
            float damageToDeal;
            TeamId teamID;
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.b);
            SpellEffectRemove(this.c);
            maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            damageToDeal = maxHealth * 0.02f;
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_NEUTRAL)
            {
                damageToDeal = Math.Min(damageToDeal, 80);
            }
            ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1.05f, ref this.lastTimeExecuted, false))
            {
                float maxHealth;
                float damageToDeal;
                TeamId teamID;
                maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                damageToDeal = maxHealth * 0.02f;
                teamID = GetTeamID(owner);
                if(teamID == TeamId.TEAM_NEUTRAL)
                {
                    damageToDeal = Math.Min(damageToDeal, 80);
                }
                ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
        }
    }
}