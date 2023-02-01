#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DisconnectTarget : BBBuffScript
    {
        public override void OnActivate()
        {
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                TeleportToKeyLocation(owner, SpawnType.SPAWN_LOCATION, TeamId.TEAM_BLUE);
            }
            else
            {
                TeleportToKeyLocation(owner, SpawnType.SPAWN_LOCATION, TeamId.TEAM_PURPLE);
            }
            IssueOrder(attacker, OrderType.MoveTo, default, owner);
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}