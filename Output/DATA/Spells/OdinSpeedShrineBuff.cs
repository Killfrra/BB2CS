#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinSpeedShrineBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "OdinSpeedShrineBuff",
            BuffTextureName = "Odin_SpeedShrine.dds",
            NonDispellable = true,
        };
        float speedMod;
        float massiveBoostOverseer;
        float massiveSpeedMod;
        Particle buffParticle;
        Particle buffParticle2;
        public OdinSpeedShrineBuff(float speedMod = default)
        {
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.speedMod);
            this.massiveBoostOverseer = 1;
            this.massiveSpeedMod = this.speedMod * 2;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.buffParticle, out _, "invis_runes_01.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.buffParticle2, out _, "Odin_Speed_Shrine_buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
            SpellEffectRemove(this.buffParticle2);
        }
        public override void OnUpdateStats()
        {
            if(this.massiveBoostOverseer < 4)
            {
                IncPercentMovementSpeedMod(owner, this.massiveSpeedMod);
                this.massiveBoostOverseer++;
            }
            else
            {
                IncPercentMovementSpeedMod(owner, this.speedMod);
            }
        }
    }
}