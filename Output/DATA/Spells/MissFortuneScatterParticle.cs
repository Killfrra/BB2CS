#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MissFortuneScatterParticle : BBBuffScript
    {
        Particle boom;
        Particle boom2;
        public override void OnActivate()
        {
            TeamId teamID;
            Particle a; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.boom, out this.boom2, "missFortune_makeItRain_tar_green.troy", "missFortune_makeItRain_tar_red.troy", teamID ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out a, out a, "missFortune_makeItRain_incoming.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out a, out a, "missFortune_makeItRain_incoming_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out a, out a, "missFortune_makeItRain_incoming_03.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out a, out a, "missFortune_makeItRain_incoming_04.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.boom);
            SpellEffectRemove(this.boom2);
        }
    }
}