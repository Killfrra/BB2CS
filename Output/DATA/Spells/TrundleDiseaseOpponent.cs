#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundleDiseaseOpponent : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleDiseaseOpponent",
            BuffTextureName = "Twitch_DeadlyVenom_temp.dds",
        };
        float debuffAmount;
        public TrundleDiseaseOpponent(float debuffAmount = default)
        {
            this.debuffAmount = debuffAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.debuffAmount);
            IncFlatArmorMod(owner, this.debuffAmount);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.debuffAmount);
        }
    }
}