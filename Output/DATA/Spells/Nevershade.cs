#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Nevershade : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Nevershade",
            BuffTextureName = "DrMundo_AdrenalineRush.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float maxHealth;
            float regen;
            float healthInc;
            if(owner.IsDead)
            {
            }
            else
            {
                if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
                {
                    maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                    regen = 0.003f;
                    healthInc = regen * maxHealth;
                    IncHealth(owner, healthInc, owner);
                }
            }
        }
    }
}