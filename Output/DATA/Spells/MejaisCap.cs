#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MejaisCap : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.15f);
        }
        public override void OnUpdateActions()
        {
            int count;
            if(ExecutePeriodically(0.3f, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MejaisCheck)) == 0)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                count = GetBuffCountFromAll(owner, nameof(Buffs.MejaisStats));
                if(count != 20)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}