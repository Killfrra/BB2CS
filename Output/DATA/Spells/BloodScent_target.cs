#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BloodScent_target : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Blood Scent",
            BuffTextureName = "Wolfman_Bloodscent.dds",
        };
        Particle particle;
        Region bubbleStuff;
        public override void OnActivate()
        {
            TeamId casterID;
            casterID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle, out _, "wolfman_bloodscent_marker.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false);
            this.bubbleStuff = AddUnitPerceptionBubble(casterID, 1000, owner, 120, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            RemovePerceptionBubble(this.bubbleStuff);
        }
    }
}