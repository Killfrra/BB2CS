#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MuramasaCap : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Aura_Offense.troy", },
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, 0.15f);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MuramasaCheck)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnUpdateActions()
        {
            int count;
            if(ExecutePeriodically(0.3f, ref this.lastTimeExecuted, false))
            {
                count = GetBuffCountFromAll(owner, nameof(Buffs.MuramasaStats));
                if(count != 20)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}