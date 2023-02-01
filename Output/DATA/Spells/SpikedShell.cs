#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SpikedShell : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spiked Shell",
            BuffTextureName = "Armordillo_ScavengeArmor.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float armorAmount;
        public override void OnActivate()
        {
            this.armorAmount = GetArmor(owner);
        }
        public override void OnUpdateStats()
        {
            float damageAmount;
            damageAmount = this.armorAmount * 0.25f;
            IncFlatPhysicalDamageMod(owner, damageAmount);
            SetBuffToolTipVar(1, damageAmount);
            SetBuffToolTipVar(2, 25);
        }
        public override void OnUpdateActions()
        {
            this.armorAmount = GetArmor(owner);
        }
    }
}