#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MuramasaStats : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MuramasaCap",
            BuffTextureName = "3034_Kenyus_Kukri.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            int count;
            float valueDisplay;
            IncFlatPhysicalDamageMod(owner, 5);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MuramasaCheck)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            count = GetBuffCountFromAll(owner, nameof(Buffs.MuramasaStats));
            valueDisplay = 5 * count;
            SetBuffToolTipVar(1, valueDisplay);
        }
    }
}