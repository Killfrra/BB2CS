#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaAriaShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "SonaRotShield_buf.troy", },
            BuffName = "SonaAriaShield",
            BuffTextureName = "Sona_PowerChord_green.dds",
        };
        float defenseBonus;
        public SonaAriaShield(float defenseBonus = default)
        {
            this.defenseBonus = defenseBonus;
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.defenseBonus);
            IncFlatSpellBlockMod(owner, this.defenseBonus);
        }
        public override void OnActivate()
        {
            //RequireVar(this.defenseBonus);
        }
    }
}