#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CassiopeiaDeathParticle : BBBuffScript
    {
        int ready;
        Particle particle1;
        float lastTimeExecuted;
        Particle particle2; // UNUSED
        int casterID; // UNUSED
        public override void OnActivate()
        {
            this.ready = 1;
            SpellEffectCreate(out this.particle1, out _, "CassiopeiaDeath.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
        }
        public override void OnUpdateActions()
        {
            Vector3 currentPos;
            Vector3 forwardPosition;
            currentPos = GetUnitPosition(owner);
            if(ExecutePeriodically(3.25f, ref this.lastTimeExecuted, false))
            {
                if(this.ready == 1)
                {
                    forwardPosition = GetPointByUnitFacingOffset(owner, 1, 220);
                    SpellEffectCreate(out this.particle2, out _, "CassDeathDust.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, "BUFFBONE_CSTM_DUST", currentPos, default, default, default, true, default, default, default, default, forwardPosition);
                    this.casterID = PushCharacterData("Cassiopeia_Death", owner, false);
                    this.ready = 2;
                }
            }
        }
    }
}