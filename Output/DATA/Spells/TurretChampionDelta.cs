#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretChampionDelta : BBBuffScript
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
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, false))
            {
                TeamId teamID;
                int numHostileChampions;
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
                    AddBuff(attacker, target, new Buffs.NegativeTurretDelta(), 1, 1, 21, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.PositiveTurretDelta));
                }
                else if(this.numAlliedChampions < numHostileChampions)
                {
                    AddBuff(attacker, target, new Buffs.PositiveTurretDelta(), 1, 1, 21, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellBuffClear(owner, nameof(Buffs.NegativeTurretDelta));
                }
                else
                {
                    SpellBuffClear(owner, nameof(Buffs.PositiveTurretDelta));
                    SpellBuffClear(owner, nameof(Buffs.NegativeTurretDelta));
                }
            }
        }
    }
}