#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IsNinja : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Is Ninja",
            BuffTextureName = "GSB_stealth.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float damageMod;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.damageMod = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, this.damageMod);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(20, ref this.lastTimeExecuted, true))
            {
                TeamId teamID;
                float numOtherNinjas;
                teamID = GetTeamID(owner);
                numOtherNinjas = -1;
                if(teamID == TeamId.TEAM_BLUE)
                {
                    foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, nameof(Buffs.IsNinja), true))
                    {
                        numOtherNinjas++;
                    }
                }
                else
                {
                    foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, nameof(Buffs.IsNinja), true))
                    {
                        numOtherNinjas++;
                    }
                }
                this.damageMod = numOtherNinjas * -1;
            }
        }
    }
}