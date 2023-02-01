#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class InnervatingLocketBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Innervating Locket",
            BuffTextureName = "3032_Innervating_Locket.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(target == attacker)
                {
                    IncPAR(owner, 10, PrimaryAbilityResourceType.MANA);
                }
            }
        }
    }
}