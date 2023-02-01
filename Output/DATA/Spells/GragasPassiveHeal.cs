#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GragasPassiveHeal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GragasPassiveHeal",
            BuffTextureName = "GragasPassiveHeal.dds",
        };
        float healAmount;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float maxHP;
            maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            this.healAmount = maxHP * 0.01f;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1.9f, ref this.lastTimeExecuted, false))
            {
                IncHealth(owner, this.healAmount, owner);
            }
        }
    }
}