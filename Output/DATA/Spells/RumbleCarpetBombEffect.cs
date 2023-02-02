#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleCarpetBombEffect : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", "", "", },
            AutoBuffActivateEffect = new[]{ "pirate_attack_buf_01.troy", "", "", },
            BuffName = "Danger Zone",
            BuffTextureName = "034_Steel_Shield.dds",
            PersistsThroughDeath = true,
        };
        int counter; // UNUSED
        Vector3 missilePosition;
        Particle particle;
        Particle particle2;
        Particle particle3;
        float lastTimeExecuted;
        public RumbleCarpetBombEffect(Vector3 missilePosition = default)
        {
            this.missilePosition = missilePosition;
        }
        public override void OnActivate()
        {
            Vector3 missilePosition;
            TeamId teamOfOwner;
            int rumbleSkinID;
            this.counter = 0;
            //RequireVar(this.missilePosition);
            missilePosition = this.missilePosition;
            teamOfOwner = GetTeamID(owner);
            rumbleSkinID = GetSkinID(attacker);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "rumble_ult_impact.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, true, default, default, missilePosition, target, default, default, true, default, default, false, false);
                if(rumbleSkinID == 2)
                {
                    SpellEffectCreate(out this.particle2, out _, "rumble_ult_impact_burn_cannon_ball_team_ID_green.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_BLUE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out this.particle3, out _, "rumble_ult_impact_burn_cannon_ball_team_ID_red.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
                else if(rumbleSkinID == 1)
                {
                    SpellEffectCreate(out this.particle2, out _, "rumble_ult_impact_burn_pineapple_team_ID_green.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_BLUE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out this.particle3, out _, "rumble_ult_impact_burn_pineapple_team_ID_red.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out this.particle2, out _, "rumble_ult_impact_burn_teamID_green.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_BLUE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out this.particle3, out _, "rumble_ult_impact_burn_teamID_red.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "rumble_ult_impact.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, true, default, default, missilePosition, target, default, default, true, default, default, false, false);
                if(rumbleSkinID == 2)
                {
                    SpellEffectCreate(out this.particle2, out _, "rumble_ult_impact_burn_cannon_ball_team_ID_red.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_BLUE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out this.particle3, out _, "rumble_ult_impact_burn_cannon_ball_team_ID_green.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
                else if(rumbleSkinID == 1)
                {
                    SpellEffectCreate(out this.particle2, out _, "rumble_ult_impact_burn_pineapple_team_ID_red.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_BLUE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out this.particle3, out _, "rumble_ult_impact_burn_pineapple_team_ID_green.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out this.particle2, out _, "rumble_ult_impact_burn_teamID_red.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_BLUE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                    SpellEffectCreate(out this.particle3, out _, "rumble_ult_impact_burn_teamID_green.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, missilePosition, target, default, default, false, default, default, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle3);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                Vector3 missilePosition;
                missilePosition = this.missilePosition;
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, missilePosition, 205, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    TeamId teamOfOwner;
                    AddBuff(attacker, unit, new Buffs.RumbleCarpetBombSlow(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    teamOfOwner = GetTeamID(attacker);
                    if(teamOfOwner == TeamId.TEAM_BLUE)
                    {
                        AddBuff(attacker, unit, new Buffs.RumbleCarpetBombBurnOrder(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, false, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, unit, new Buffs.RumbleCarpetBombBurnDest(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, false, false, false);
                    }
                }
            }
        }
    }
}