#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_GrandSkyfall : BBBuffScript
    {
        Vector3 targetPos;
        Region bubbleID;
        public Pantheon_GrandSkyfall(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            //RequireVar(this.targetPos);
            //RequireVar(this.particle);
            targetPos = this.targetPos;
            teamOfOwner = GetTeamID(owner);
            this.bubbleID = AddPosPerceptionBubble(teamOfOwner, 700, targetPos, 6, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
    }
}