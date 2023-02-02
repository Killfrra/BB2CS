#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVDemacianStandardBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVDemacianStandard",
            BuffTextureName = "JarvanIV_DemacianStandard.dds",
        };
        float attackSpeedMod;
        float armorMod;
        Particle asdf;
        public JarvanIVDemacianStandardBuff(float attackSpeedMod = default, float armorMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            TeamId teamID; // UNITIALIZED
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.armorMod);
            SpellEffectCreate(out this.asdf, out _, "JarvanDemacianStandard_shield.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.asdf);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncFlatArmorMod(owner, this.armorMod);
        }
    }
}