#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SummonerBattleCryBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "root", },
            AutoBuffActivateEffect = new[]{ "OntheHunt_buf.troy", "", },
            BuffName = "SummonerBattleCry",
            BuffTextureName = "Summoner_BattleCry.dds",
        };
        float allyAPMod;
        float allyAttackSpeedMod;
        public SummonerBattleCryBuff(float allyAPMod = default, float allyAttackSpeedMod = default)
        {
            this.allyAPMod = allyAPMod;
            this.allyAttackSpeedMod = allyAttackSpeedMod;
        }
        public override void OnActivate()
        {
            int level; // UNUSED
            level = GetLevel(owner);
            //RequireVar(this.allyAPMod);
            //RequireVar(this.allyAttackSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncScaleSkinCoef(0.1f, owner);
            IncFlatMagicDamageMod(owner, this.allyAPMod);
            IncPercentAttackSpeedMod(owner, this.allyAttackSpeedMod);
        }
    }
}