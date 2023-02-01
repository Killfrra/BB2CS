#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretBonusHealth : BBBuffScript
    {
        float bonusHealth;
        float bubbleSize;
        Region thisBubble; // UNUSED
        public TurretBonusHealth(float bonusHealth = default, float bubbleSize = default)
        {
            this.bonusHealth = bonusHealth;
            this.bubbleSize = bubbleSize;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusHealth);
            //RequireVar(this.bubbleSize);
        }
        public override void OnUpdateStats()
        {
            TeamId teamID;
            float numChampions;
            float bonusHealth;
            teamID = GetTeamID(owner);
            this.thisBubble = AddUnitPerceptionBubble(teamID, this.bubbleSize, owner, 25000, default, default, true);
            if(teamID == TeamId.TEAM_BLUE)
            {
                numChampions = GetNumberOfHeroesOnTeam(TeamId.TEAM_PURPLE, true, true);
            }
            else
            {
                numChampions = GetNumberOfHeroesOnTeam(TeamId.TEAM_BLUE, true, true);
            }
            numChampions = Math.Min(5, numChampions);
            bonusHealth = numChampions * this.bonusHealth;
            IncPermanentFlatHPPoolMod(owner, bonusHealth);
            SpellBuffRemoveCurrent(owner);
        }
    }
}