#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlackOmen : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BlackOmen",
            BuffTextureName = "3143_Randuins_Omen.dds",
        };
        float attackSpeedMod;
        public BlackOmen(float attackSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}