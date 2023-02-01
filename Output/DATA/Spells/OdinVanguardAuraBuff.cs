#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinVanguardAuraBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VanguardBuff",
            BuffTextureName = "Summoner_rally.dds",
        };
        float defenseMod;
        public override void OnActivate()
        {
            int ownerLevel;
            ownerLevel = GetLevel(attacker);
            this.defenseMod = 5 * ownerLevel;
            this.defenseMod += 10;
            ApplyAssistMarker(attacker, owner, 10);
            SetBuffToolTipVar(1, this.defenseMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, this.defenseMod);
            IncFlatArmorMod(owner, this.defenseMod);
        }
    }
}