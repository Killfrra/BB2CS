#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVDragonStrikeDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVDragonStrikeDebuff",
            BuffTextureName = "JarvanIV_DragonStrike.dds",
        };
        float armorDebuff;
        Particle particle1;
        Particle hitParticle;
        public JarvanIVDragonStrikeDebuff(float armorDebuff = default)
        {
            this.armorDebuff = armorDebuff;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            //RequireVar(this.armorDebuff);
            IncPercentArmorMod(owner, this.armorDebuff);
            SpellEffectCreate(out this.particle1, out _, "JarvanDragonStrike_debuff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.hitParticle, out _, "JarvanDragonStrike_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
            SpellEffectRemove(this.hitParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, this.armorDebuff);
        }
    }
}