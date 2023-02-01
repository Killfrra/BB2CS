#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinQuestIndicator : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCenterShrineBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Particle particle;
        Particle particle2;
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out _, "odin_point_active.troy", default, TeamId.TEAM_BLUE, 10, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particle2, out _, "odin_point_active.troy", default, TeamId.TEAM_PURPLE, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
    }
}