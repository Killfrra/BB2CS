#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LandslideDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LandslideDebuff",
            BuffTextureName = "Malphite_GroundSlam.dds",
        };
        int level;
        Particle landslideLHand;
        Particle landslideRHand;
        float[] effect0 = {-0.3f, -0.35f, -0.4f, -0.45f, -0.5f};
        public LandslideDebuff(int level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
            SpellEffectCreate(out this.landslideLHand, out _, "Landslide_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, target, default, default, false);
            SpellEffectCreate(out this.landslideRHand, out _, "Landslide_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.landslideLHand);
            SpellEffectRemove(this.landslideRHand);
        }
        public override void OnUpdateStats()
        {
            int level;
            level = this.level;
            IncPercentMultiplicativeAttackSpeedMod(owner, this.effect0[level]);
        }
    }
}