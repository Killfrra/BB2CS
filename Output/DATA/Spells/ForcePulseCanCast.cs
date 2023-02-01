#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ForcePulseCanCast : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "ForcewalkReady.troy", },
            BuffName = "ForcePulseAvailable",
            BuffTextureName = "Kassadin_ForcePulse.dds",
        };
        public override void OnActivate()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true);
        }
    }
}