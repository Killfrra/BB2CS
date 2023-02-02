#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleHeatPunch : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Feel No Pain",
            BuffTextureName = "Sion_FeelNoPain.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float punchdmg;
        float lastTimeExecuted;
        int[] effect0 = {25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110};
        public override void OnActivate()
        {
            this.punchdmg = 0;
            OverrideAutoAttack(5, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
            CancelAutoAttack(owner, false);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(5, ref this.lastTimeExecuted, true))
            {
                int level;
                level = GetLevel(owner);
                this.punchdmg = this.effect0[level];
                SetBuffToolTipVar(1, this.punchdmg);
            }
        }
    }
}