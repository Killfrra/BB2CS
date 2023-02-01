#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PendantofZephirisAuraSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Aegis_buf.troy", },
            BuffName = "Pendant of Zephiris",
            BuffTextureName = "082_Rune_of_Rebirth.dds",
        };
        float magicResistBonus;
        float armorBonus;
        public PendantofZephirisAuraSelf(float magicResistBonus = default, float armorBonus = default)
        {
            this.magicResistBonus = magicResistBonus;
            this.armorBonus = armorBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.magicResistBonus);
            //RequireVar(this.armorBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, this.magicResistBonus);
            IncFlatArmorMod(owner, this.armorBonus);
        }
    }
}