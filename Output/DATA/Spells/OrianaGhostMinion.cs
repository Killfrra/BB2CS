#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaGhostMinion : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        Region tempVision;
        Particle ring2;
        Particle ring1;
        Particle ring4;
        Particle ring3;
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 ownerPos;
            teamID = GetTeamID(owner);
            ownerPos = GetUnitPosition(owner);
            SetGhosted(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetForceRenderParticles(owner, true);
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
            this.tempVision = AddPosPerceptionBubble(teamID, 225, ownerPos, 25000, default, false);
            SpellEffectCreate(out this.ring2, out this.ring1, "yomu_ring_green.troy", "yomu_ring_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, ownerPos, owner, default, ownerPos, false, default, default, false, false);
            SpellEffectCreate(out this.ring4, out this.ring3, "oriana_ball_glow_green.troy", "oriana_ball_glow_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "balldown", ownerPos, owner, default, ownerPos, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, true);
            SetInvulnerable(owner, false);
            RemovePerceptionBubble(this.tempVision);
            SpellEffectRemove(this.ring1);
            SpellEffectRemove(this.ring2);
            SpellEffectRemove(this.ring3);
            SpellEffectRemove(this.ring4);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ExpirationTimer(), 1, 1, 0.249f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}