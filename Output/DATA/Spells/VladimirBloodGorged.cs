#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VladimirBloodGorged : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VladimirBloodGorged",
            BuffTextureName = "Vladimir_BloodGorged.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float hPMod;
        float aPMod;
        public override void OnActivate()
        {
            this.hPMod = 0;
            this.aPMod = 0;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, this.aPMod);
            IncMaxHealth(owner, this.hPMod, false);
            SetBuffToolTipVar(2, this.hPMod);
            SetBuffToolTipVar(1, this.aPMod);
        }
        public override void OnUpdateActions()
        {
            float currentHP;
            float currentAP;
            currentHP = GetFlatHPPoolMod(owner);
            currentAP = GetFlatMagicDamageMod(owner);
            currentAP -= this.aPMod;
            this.aPMod = currentHP * 0.025f;
            this.hPMod = currentAP * 1.4f;
        }
    }
}