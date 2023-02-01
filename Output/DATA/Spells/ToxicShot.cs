#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ToxicShot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Toxic Attack",
            BuffTextureName = "Teemo_PoisonedDart.dds",
            SpellFXOverrideSkins = new[]{ "AstronautTeemo", },
            SpellToggleSlot = 3,
        };
        public override void OnActivate()
        {
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
        }
    }
}