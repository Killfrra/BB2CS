#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SpiritVisage_visible : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spirit Visage",
            BuffTextureName = "3065_Spirit_Visage.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float healthPercent;
            float maxHealth;
            float healthToInc;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                if(healthPercent <= 0.4f)
                {
                    maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                    healthToInc = maxHealth * 0.01f;
                    IncHealth(owner, healthToInc, owner);
                }
            }
        }
    }
}