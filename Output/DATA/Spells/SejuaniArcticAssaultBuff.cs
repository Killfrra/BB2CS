#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniArcticAssaultBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "sejuani_arctic_assault_buf.troy", },
            BuffName = "SejuaniArcticAssaultBuff",
            BuffTextureName = "Annie_GhastlyShield.dds",
        };
        float defenses;
        public SejuaniArcticAssaultBuff(float defenses = default)
        {
            this.defenses = defenses;
        }
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            teamID = GetTeamID(owner);
            //RequireVar(this.defenses);
            IncFlatSpellBlockMod(owner, this.defenses);
            IncFlatArmorMod(owner, this.defenses);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, this.defenses);
            IncFlatArmorMod(owner, this.defenses);
        }
    }
}