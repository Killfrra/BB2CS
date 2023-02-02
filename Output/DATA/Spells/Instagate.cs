#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Instagate : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 100f, 85f, 70f, 55f, 40f, },
            ChannelDuration = 4f,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        Particle particle; // UNUSED
        Particle particle2; // UNUSED
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            Particle gateParticle; // UNITIALIZED
            Particle nextBuffVars_GateParticle;
            Vector3 nextBuffVars_CurrentPos; // UNUSED
            targetPos = GetCastSpellTargetPos();
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "CardmasterTeleport_red.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, target, default, default, false);
                SpellEffectCreate(out this.particle, out _, "GateMarker_red.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, targetPos, target, default, default, false);
                SpellEffectCreate(out this.particle2, out _, "GateMarker_green.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_BLUE, default, default, true, default, default, targetPos, target, default, default, false);
                SpellEffectCreate(out this.particle2, out _, "CardmasterTeleport_green.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, target, default, default, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "CardmasterTeleport_red.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, target, default, default, false);
                SpellEffectCreate(out this.particle, out _, "GateMarker_red.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_BLUE, default, default, true, default, default, targetPos, target, default, default, false);
                SpellEffectCreate(out this.particle2, out _, "GateMarker_green.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_PURPLE, default, default, true, default, default, targetPos, target, default, default, false);
                SpellEffectCreate(out this.particle2, out _, "CardmasterTeleport_green.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, target, default, default, false);
            }
            nextBuffVars_GateParticle = gateParticle;
            nextBuffVars_CurrentPos = GetUnitPosition(owner);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Instagate(nextBuffVars_GateParticle), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
        public override void ChannelingSuccessStop()
        {
            Vector3 castPosition; // UNITIALIZED
            TeleportToPosition(owner, castPosition);
            SpellEffectCreate(out _, out _, "CardmasterTeleportArrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SpellBuffRemove(owner, nameof(Buffs.Gate), (ObjAIBase)owner);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Gate), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.Instagate), (ObjAIBase)owner);
        }
    }
}
namespace Buffs
{
    public class Instagate : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Gate",
            BuffTextureName = "Cardmaster_Premonition.dds",
        };
        Particle gateParticle;
        Particle gateParticle2;
        float lastTimeExecuted;
        public Instagate(Particle gateParticle = default, Particle gateParticle2 = default)
        {
            this.gateParticle = gateParticle;
            this.gateParticle2 = gateParticle2;
        }
        public override void OnActivate()
        {
            //RequireVar(this.gateParticle);
            //RequireVar(this.gateParticle2);
            //RequireVar(this.currentPos);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(2.675f, ref this.lastTimeExecuted, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.GateFix(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.gateParticle);
            SpellEffectRemove(this.gateParticle2);
        }
    }
}