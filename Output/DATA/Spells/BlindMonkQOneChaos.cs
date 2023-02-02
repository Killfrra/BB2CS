#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkQOneChaos : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "global_Watched.troy", },
            BuffName = "BlindMonkSonicWave",
            BuffTextureName = "BlindMonkQOne.dds",
        };
        Region bubbleID;
        Region bubbleID2;
        Particle slow;
        public override void OnActivate()
        {
            TeamId teamID;
            Particle hit1; // UNUSED
            Particle blood; // UNUSED
            teamID = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(teamID, 400, owner, 20, default, default, false);
            this.bubbleID2 = AddUnitPerceptionBubble(teamID, 50, owner, 20, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out hit1, out _, "blindMonk_Q_resonatingStrike_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false);
            SpellEffectCreate(out blood, out _, "blindMonk_Q_resonatingStrike_tar_blood.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false);
            SpellEffectCreate(out this.slow, out _, "blindMonk_Q_tar_indicator.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            RemovePerceptionBubble(this.bubbleID2);
            SpellEffectRemove(this.slow);
            if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.BlindMonkQManager)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.BlindMonkQManager), (ObjAIBase)owner);
            }
        }
    }
}