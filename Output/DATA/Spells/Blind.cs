#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Blind : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Global_miss.troy", },
            BuffName = "Blind",
            BuffTextureName = "BlindMonk_SightUnseeing.dds",
        };
        float missChance;
        public Blind(float missChance = default)
        {
            this.missChance = missChance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.missChance);
            CancelAutoAttack(owner, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatMissChanceMod(owner, this.missChance);
        }
    }
}