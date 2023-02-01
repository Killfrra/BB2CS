#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class AttackDamageTalent : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float damageInc;
            damageInc = 100 * talentLevel;
            IncFlatPhysicalDamageMod(owner, damageInc);
            if(ExecutePeriodically(1, ref this.lastTimeExecuted))
            {
                DebugSay(owner, "DamageInc: ", damageInc);
            }
        }
    }
}