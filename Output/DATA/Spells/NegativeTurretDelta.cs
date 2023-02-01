#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NegativeTurretDelta : BBBuffScript
    {
        float percentBonus;
        float startTime;
        public override void OnActivate()
        {
            this.percentBonus = 0;
            this.startTime = GetGameTime();
        }
        public override void OnUpdateStats()
        {
            float currentTime;
            float timeDelta;
            float timePercent;
            currentTime = GetGameTime();
            timeDelta = currentTime - this.startTime;
            timeDelta = Math.Min(timeDelta, 90);
            timePercent = timeDelta / 90;
            this.percentBonus = -0.05f * timePercent;
            IncPercentPhysicalReduction(owner, this.percentBonus);
        }
    }
}