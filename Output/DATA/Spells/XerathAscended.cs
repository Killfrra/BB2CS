#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XerathAscended : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XerathAscended",
            BuffTextureName = "Xerath_AscendedForm.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float aPMod;
        public override void OnActivate()
        {
            this.aPMod = GetFlatMagicDamageMod(owner);
            SetBuffToolTipVar(2, 15);
        }
        public override void OnUpdateStats()
        {
            float armorBonus;
            armorBonus = this.aPMod * 0.15f;
            IncFlatArmorMod(owner, armorBonus);
            SetBuffToolTipVar(1, armorBonus);
        }
        public override void OnUpdateActions()
        {
            this.aPMod = GetFlatMagicDamageMod(owner);
        }
    }
}