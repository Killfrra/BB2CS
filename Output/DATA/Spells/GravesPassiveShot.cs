#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GravesPassiveShot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "head", },
            AutoBuffActivateEffect = new[]{ "", "RighteousFuryHalo_buf.troy", },
            BuffName = "JudicatorRighteousFury",
            BuffTextureName = "Judicator_RighteousFury.dds",
        };
        public override void OnActivate()
        {
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
        }
    }
}