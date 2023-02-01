#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ConsecrationAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Consecration Self",
            BuffTextureName = "Soraka_Consecration.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            IncPermanentFlatSpellBlockMod(owner, 16);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentFlatSpellBlockMod(owner, -16);
        }
    }
}