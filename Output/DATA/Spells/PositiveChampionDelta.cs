#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PositiveChampionDelta : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float percentBonus;
        float startTime;
        float expPercentBonus;
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
            this.percentBonus = 0.1f * timePercent;
            IncPercentRespawnTimeMod(owner, this.percentBonus);
            this.expPercentBonus = 0.05f * timePercent;
            IncPercentEXPBonus(owner, this.expPercentBonus);
        }
    }
}