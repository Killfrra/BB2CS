#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EmpoweredBulwark : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Empowered Bulwark",
            BuffTextureName = "ChemicalMan_EmpoweredBulwark.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float tempMana;
        public override void OnActivate()
        {
            this.tempMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            float healthMod;
            float healthIncRate;
            healthMod = this.tempMana * 0.25f;
            healthIncRate = 0 + 2.5f;
            IncFlatHPPoolMod(owner, healthMod);
            SetBuffToolTipVar(1, healthMod);
            SetBuffToolTipVar(2, healthIncRate);
            SetBuffToolTipVar(3, 10);
        }
        public override void OnUpdateActions()
        {
            this.tempMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
    }
}