#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MejaisStats : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MejaisCap",
            BuffTextureName = "3041_Mejais_Soulstealer.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            int count;
            float aPDisplay;
            IncFlatMagicDamageMod(owner, 8);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MejaisCheck)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            count = GetBuffCountFromAll(owner, nameof(Buffs.MejaisStats));
            aPDisplay = 8 * count;
            SetBuffToolTipVar(1, aPDisplay);
        }
    }
}