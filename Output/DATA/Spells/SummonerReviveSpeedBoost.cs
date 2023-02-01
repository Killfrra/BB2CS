#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerReviveSpeedBoost : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "ArmordilloSpin.troy", "Powerball_buf.troy", },
            AutoBuffActivateEffectFlags = EffCreate.UPDATE_ORIENTATION,
            BuffName = "SummonerReviveSpeedBoost",
            BuffTextureName = "Summoner_revive.dds",
            SpellToggleSlot = 1,
        };
        float moveSpeedMod;
        public SummonerReviveSpeedBoost(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            this.moveSpeedMod -= 0.026f;
            if(this.moveSpeedMod < 0)
            {
                this.moveSpeedMod = 0;
            }
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}