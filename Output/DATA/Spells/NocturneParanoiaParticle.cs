#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneParanoiaParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            TeamId teamOfOwner;
            float duration;
            FadeOutColorFadeEffect(1, TeamId.TEAM_UNKNOWN);
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, nameof(Buffs.NocturneParanoiaParticle), true))
                {
                    duration = GetBuffRemainingDuration(unit, nameof(Buffs.NocturneParanoiaParticle));
                    if(duration > 0.5f)
                    {
                        FadeInColorFadeEffect(75, 0, 0, 1, 0.3f, TeamId.TEAM_BLUE);
                    }
                }
            }
            else
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, nameof(Buffs.NocturneParanoiaParticle), true))
                {
                    duration = GetBuffRemainingDuration(unit, nameof(Buffs.NocturneParanoiaParticle));
                    if(duration > 0.5f)
                    {
                        FadeInColorFadeEffect(75, 0, 0, 1, 0.3f, TeamId.TEAM_PURPLE);
                    }
                }
            }
        }
    }
}