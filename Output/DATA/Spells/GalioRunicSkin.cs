#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GalioRunicSkin : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GalioRunicSkin",
            BuffTextureName = "Galio_RunicSkin.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float totalMR;
        public override void OnActivate()
        {
            this.totalMR = GetSpellBlock(owner);
        }
        public override void OnUpdateStats()
        {
            float aPMod;
            aPMod = this.totalMR * 0.5f;
            IncFlatMagicDamageMod(owner, aPMod);
            SetBuffToolTipVar(1, aPMod);
        }
        public override void OnUpdateActions()
        {
            this.totalMR = GetSpellBlock(owner);
        }
    }
}