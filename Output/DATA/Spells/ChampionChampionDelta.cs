#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ChampionChampionDelta : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int numAlliedChampions;
        public override void OnUpdateStats()
        {
            TeamId teamID;
            int numHostileChampions;
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, false))
            {
                teamID = GetTeamID(owner);
                if(teamID == TeamId.TEAM_BLUE)
                {
                    this.numAlliedChampions = GetNumberOfHeroesOnTeam(TeamId.TEAM_BLUE, false, true);
                    numHostileChampions = GetNumberOfHeroesOnTeam(TeamId.TEAM_PURPLE, false, true);
                }
                else if(teamID == TeamId.TEAM_PURPLE)
                {
                    this.numAlliedChampions = GetNumberOfHeroesOnTeam(TeamId.TEAM_PURPLE, false, true);
                    numHostileChampions = GetNumberOfHeroesOnTeam(TeamId.TEAM_BLUE, false, true);
                }
                if(this.numAlliedChampions > numHostileChampions)
                {
                    SpellBuffClear(owner, nameof(Buffs.PositiveChampionDelta));
                }
                else if(this.numAlliedChampions < numHostileChampions)
                {
                    AddBuff(attacker, target, new Buffs.PositiveChampionDelta(), 1, 1, 21, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else
                {
                    SpellBuffClear(owner, nameof(Buffs.PositiveChampionDelta));
                }
            }
        }
    }
}