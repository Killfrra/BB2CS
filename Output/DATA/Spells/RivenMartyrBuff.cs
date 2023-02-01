#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenMartyrBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[]{ "enrage_buf.troy", "enrage_buf.troy", },
            BuffName = "BlindMonkSafeguard",
        };
        float speedBoost;
        public RivenMartyrBuff(float speedBoost = default)
        {
            this.speedBoost = speedBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedBoost);
            IncPercentAttackSpeedMod(owner, this.speedBoost);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.speedBoost);
        }
    }
}