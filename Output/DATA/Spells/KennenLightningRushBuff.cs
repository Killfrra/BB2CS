#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KennenLightningRushBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "KennenLightningRushBuff",
            BuffTextureName = "Kennen_LightningRush.dds",
            SpellToggleSlot = 3,
        };
        float defenseBonus;
        public KennenLightningRushBuff(float defenseBonus = default)
        {
            this.defenseBonus = defenseBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.defenseBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.defenseBonus);
            IncFlatSpellBlockMod(owner, this.defenseBonus);
            SetBuffToolTipVar(1, this.defenseBonus);
        }
    }
}