#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretBonusHQ : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Magical Sight",
            BuffTextureName = "096_Eye_of_the_Observer.dds",
        };
        float maxIncreases;
        float damageMod;
        float armorMod;
        float resistMod;
        float loopOffset;
        float startDecay;
        float maximumArmor;
        float maximumResist;
        float maximumDamage;
        Region thisBubble;
        float bonusHealth;
        float looper;
        float lastTimeExecuted;
        public TurretBonusHQ(float maxIncreases = default, float loopOffset = default, float startDecay = default)
        {
            this.maxIncreases = maxIncreases;
            this.loopOffset = loopOffset;
            this.startDecay = startDecay;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            float numChampions;
            //RequireVar(this.maxIncreases);
            //RequireVar(this.damageMod);
            //RequireVar(this.armorMod);
            //RequireVar(this.resistMod);
            //RequireVar(this.loopOffset);
            //RequireVar(this.startDecay);
            this.maximumArmor = 2.5f * this.maxIncreases;
            this.maximumResist = 2.5f * this.maxIncreases;
            this.maximumDamage = 7 * this.maxIncreases;
            teamID = GetTeamID(owner);
            this.thisBubble = AddUnitPerceptionBubble(teamID, 800, owner, 25000, default, default, true);
            if(teamID == TeamId.TEAM_BLUE)
            {
                numChampions = GetNumberOfHeroesOnTeam(TeamId.TEAM_PURPLE, true, true);
            }
            else
            {
                numChampions = GetNumberOfHeroesOnTeam(TeamId.TEAM_BLUE, true, true);
            }
            numChampions = Math.Min(5, numChampions);
            this.bonusHealth = numChampions * 150;
            this.looper = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.thisBubble);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageMod);
            IncFlatArmorMod(owner, this.armorMod);
            IncFlatSpellBlockMod(owner, this.resistMod);
            if(ExecutePeriodically(60, ref this.lastTimeExecuted, false))
            {
                if(this.looper >= this.startDecay)
                {
                    this.armorMod -= 5;
                    this.resistMod -= 5;
                }
                else if(this.looper >= this.loopOffset)
                {
                    this.armorMod += 2.5f;
                    this.resistMod += 2.5f;
                    this.damageMod += 7;
                    this.damageMod = Math.Min(this.damageMod, this.maximumDamage);
                    this.armorMod = Math.Min(this.armorMod, this.maximumArmor);
                    this.resistMod = Math.Min(this.resistMod, this.maximumResist);
                    this.looper++;
                }
                else
                {
                    this.looper++;
                }
            }
            IncFlatHPPoolMod(owner, this.bonusHealth);
        }
    }
}