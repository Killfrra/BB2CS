#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MasteryHoardBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "FortifyBuff",
            BuffTextureName = "Summoner_fortify.dds",
            PersistsThroughDeath = true,
        };
        float goldAmount;
        public MasteryHoardBuff(float goldAmount = default)
        {
            this.goldAmount = goldAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.goldAmount);
            IncGold(owner, this.goldAmount);
        }
    }
}