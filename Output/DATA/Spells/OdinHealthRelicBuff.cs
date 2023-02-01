#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinHealthRelicBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Regenerationpotion_itm.troy", },
            BuffName = "OdinHealthRelic",
            BuffTextureName = "2003_Regeneration_Potion.dds",
        };
        float healPerTick;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float baseHeal;
            int level;
            float perLevelHeal;
            float totalHeal;
            baseHeal = 80;
            level = GetLevel(owner);
            perLevelHeal = 25 * level;
            totalHeal = perLevelHeal + baseHeal;
            this.healPerTick = totalHeal / 10;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                IncHealth(owner, this.healPerTick, owner);
            }
        }
    }
}