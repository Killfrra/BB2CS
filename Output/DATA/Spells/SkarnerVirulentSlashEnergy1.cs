#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SkarnerVirulentSlashEnergy1 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "L_crystals", "R_crystals", "tail_t", },
            AutoBuffActivateEffect = new[]{ "Skarner_Crystal_Slash_Buff.troy", "Skarner_Crystal_Slash_Buff.troy", "Skarner_Crystal_Slash_Buff.troy", },
        };
        Particle particle1;
        Particle particle2;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.particle1, out _, "Skarner_Crystal_Slash_Activate_L.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, default, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.particle2, out _, "Skarner_Crystal_Slash_Activate_R.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, default, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
            SpellEffectRemove(this.particle2);
        }
    }
}