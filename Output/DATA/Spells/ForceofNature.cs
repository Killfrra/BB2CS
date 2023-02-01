#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ForceofNature : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForceofNature",
            BuffTextureName = "124_Gladiators_Pride.dds",
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
                if(healthPercent <= 1)
                {
                    maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                    healthToInc = maxHealth * 0.0035f;
                    IncHealth(owner, healthToInc, owner);
                }
            }
        }
    }
}