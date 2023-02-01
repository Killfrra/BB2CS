#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeviathanCap : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Aegis_buf.troy", },
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, 0.15f);
            IncPercentMagicReduction(owner, 0.15f);
        }
        public override void OnUpdateActions()
        {
            int count;
            if(ExecutePeriodically(0.9f, ref this.lastTimeExecuted, false))
            {
                count = GetBuffCountFromAll(owner, nameof(Buffs.LeviathanStats));
                if(count != 20)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeviathanCheck)) == 0)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}