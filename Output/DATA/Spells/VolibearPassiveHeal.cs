#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearPassiveHeal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "volibear_passive_heal.troy", },
            BuffName = "VolibearPassiveHeal",
            BuffTextureName = "VolibearPassive.dds",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float maxHealth;
            float factor;
            float heal;
            maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            factor = charVars.RegenPercent * 0.08333f;
            heal = factor * maxHealth;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                IncHealth(owner, heal, owner);
            }
        }
    }
}