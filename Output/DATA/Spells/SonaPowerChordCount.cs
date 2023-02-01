#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaPowerChordCount : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaPowerChordCount",
            BuffTextureName = "Sona_PowerChord.dds",
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChordCount));
            if(count >= 3)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.SonaPowerChord(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.SonaPowerChordCount), 0);
            }
        }
    }
}