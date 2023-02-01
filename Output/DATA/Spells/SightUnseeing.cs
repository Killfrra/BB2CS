#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SightUnseeing : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SightUnseeing",
            BuffTextureName = "BlindMonk_SightUnseeing.dds",
        };
        Region thisBubble;
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(charVars.BubbleRadius);
            teamID = GetTeamID(owner);
            this.thisBubble = AddUnitPerceptionBubble(teamID, charVars.BubbleRadius, owner, 9999, owner, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.thisBubble);
        }
    }
}