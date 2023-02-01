#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CassiopeiaDeadlyCadence : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "CassiopeiaDeadlyCadence",
            BuffTextureName = "Cassiopeia_DeadlyCadence.dds",
            NonDispellable = true,
        };
        float percentMod;
        public override void OnActivate()
        {
            float curCost;
            float cost;
            float tooltip;
            this.percentMod = -0.1f;
            curCost = GetPARMultiplicativeCostInc(owner, 0, SpellSlotType.SpellSlots, PrimaryAbilityResourceType.MANA);
            cost = curCost + this.percentMod;
            tooltip = cost * -100;
            SetBuffToolTipVar(1, tooltip);
            SetPARMultiplicativeCostInc(owner, 0, SpellSlotType.SpellSlots, cost, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 1, SpellSlotType.SpellSlots, cost, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 2, SpellSlotType.SpellSlots, cost, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 3, SpellSlotType.SpellSlots, cost, PrimaryAbilityResourceType.MANA);
        }
        public override void OnDeactivate(bool expired)
        {
            SetPARMultiplicativeCostInc(owner, 0, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 2, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARMultiplicativeCostInc(owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
    }
}