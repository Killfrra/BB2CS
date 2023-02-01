#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ForcePulseCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForcePulse",
            BuffTextureName = "Kassadin_ForcePulse_Charging.dds",
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.ForcePulseCounter));
            if(count >= 6)
            {
                AddBuff(attacker, attacker, new Buffs.ForcePulseCanCast(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.ForcePulseCounter), 0);
            }
        }
    }
}