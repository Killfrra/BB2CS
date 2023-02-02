#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaFireballDragonMinion : BBBuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID;
            Particle a; // UNUSED
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "Incinerate_cas.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "top", default, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
        }
    }
}