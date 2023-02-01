#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pyromania : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pyromania",
            BuffTextureName = "Annie_Brilliance_Charging.dds",
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pyromania));
            if(count >= 5)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Pyromania_particle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.Pyromania), 0);
            }
        }
    }
}