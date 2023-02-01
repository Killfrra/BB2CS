#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeviathanStats : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeviathanCap",
            BuffTextureName = "3138_Leviathan.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            int count;
            float healthDisplay;
            IncFlatHPPoolMod(owner, 32);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeviathanCheck)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            count = GetBuffCountFromAll(owner, nameof(Buffs.LeviathanStats));
            healthDisplay = 32 * count;
            SetBuffToolTipVar(1, healthDisplay);
        }
    }
}