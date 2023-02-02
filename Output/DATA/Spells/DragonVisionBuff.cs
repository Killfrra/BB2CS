#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DragonVisionBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Dragon Vision Buff",
            BuffTextureName = "Summoner_clairvoyance.dds",
        };
        Vector3 particlePosition;
        Particle castParticle;
        Region bubble;
        public DragonVisionBuff(Vector3 particlePosition = default)
        {
            this.particlePosition = particlePosition;
        }
        public override void OnActivate()
        {
            if(owner is Champion)
            {
                Vector3 particlePosition;
                TeamId teamID;
                particlePosition = this.particlePosition;
                teamID = GetTeamID(owner);
                SpellEffectCreate(out this.castParticle, out _, "TwistedTreelineClairvoyance.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, particlePosition, target, default, default, false);
                if(teamID == TeamId.TEAM_BLUE)
                {
                    this.bubble = AddPosPerceptionBubble(teamID, 1150, particlePosition, 90, default, false);
                }
                if(teamID == TeamId.TEAM_PURPLE)
                {
                    this.bubble = AddPosPerceptionBubble(teamID, 1150, particlePosition, 90, default, false);
                }
            }
            else
            {
                this.particlePosition = GetUnitPosition(owner);
            }
            //RequireVar(this.particlePosition);
        }
        public override void OnDeactivate(bool expired)
        {
            if(owner is Champion)
            {
                SpellEffectRemove(this.castParticle);
                RemovePerceptionBubble(this.bubble);
            }
        }
        public override void OnDeath()
        {
            if(owner is not Champion)
            {
                Vector3 nextBuffVars_ParticlePosition;
                nextBuffVars_ParticlePosition = this.particlePosition;
                AddBuff(attacker, attacker, new Buffs.DragonVisionBuff(nextBuffVars_ParticlePosition), 1, 1, 90, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}