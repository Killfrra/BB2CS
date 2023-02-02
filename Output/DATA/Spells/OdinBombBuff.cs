#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinBombBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "OdinShrineBombBuff",
            BuffTextureName = "DrMundo_BurningAgony.dds",
        };
        Region bubbleID;
        Region bubbleID2;
        Particle buffParticle;
        Particle buffParticle2;
        Particle crystalParticle;
        Particle crystalParticle2;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID;
            TeamId orderTeam;
            TeamId chaosTeam;
            teamID = GetTeamID(owner);
            SetCanMove(owner, false);
            SetForceRenderParticles(owner, true);
            SetTargetable(owner, false);
            orderTeam = TeamId.TEAM_BLUE;
            chaosTeam = TeamId.TEAM_PURPLE;
            this.bubbleID = AddUnitPerceptionBubble(orderTeam, 350, owner, 25000, default, default, true);
            this.bubbleID2 = AddUnitPerceptionBubble(chaosTeam, 350, owner, 25000, default, default, true);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.buffParticle, out this.buffParticle2, "odin_relic_buf_red.troy", "odin_relic_buf_green.troy", TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, true, false, false, false);
                SpellEffectCreate(out this.crystalParticle, out this.crystalParticle2, "Odin_Prism_Red.troy", "Odin_Prism_Green.troy", TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, true, true, false, false);
            }
            else
            {
                SpellEffectCreate(out this.buffParticle, out this.buffParticle2, "odin_relic_buf_green.troy", "odin_relic_buf_red.troy", TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, true, false, false, false);
                SpellEffectCreate(out this.crystalParticle, out this.crystalParticle2, "Odin_Prism_Green.troy", "Odin_Prism_Red.troy", TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, true, true, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            RemovePerceptionBubble(this.bubbleID2);
            SpellEffectRemove(this.buffParticle);
            SpellEffectRemove(this.buffParticle2);
            SpellEffectRemove(this.crystalParticle);
            SpellEffectRemove(this.crystalParticle2);
        }
        public override void OnUpdateStats()
        {
            float healthPercent;
            float size; // UNUSED
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            healthPercent = GetPARPercent(target, PrimaryAbilityResourceType.MANA);
            size = 350 * healthPercent;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                int count;
                count = GetBuffCountFromAll(owner, nameof(Buffs.OdinBombSuppression));
                if(count == 0)
                {
                    TeamId teamID; // UNUSED
                    float healAmount;
                    teamID = GetTeamID(owner);
                    healAmount = 20000;
                    IncPAR(owner, healAmount, PrimaryAbilityResourceType.MANA);
                }
            }
        }
    }
}