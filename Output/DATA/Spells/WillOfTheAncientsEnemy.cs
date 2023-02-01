#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WillOfTheAncientsEnemy : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WillOfTheAncientsEnemy",
            BuffTextureName = "2008_Tome_of_Combat_Mastery.dds",
        };
        float aP_Debuff;
        public WillOfTheAncientsEnemy(float aP_Debuff = default)
        {
            this.aP_Debuff = aP_Debuff;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aP_Debuff);
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, this.aP_Debuff);
        }
    }
}