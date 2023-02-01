#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiUnstableGrowthArmor : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiTrunkSmash",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
        float defensiveBonus;
        Particle taric;
        public MaokaiUnstableGrowthArmor(float defensiveBonus = default)
        {
            this.defensiveBonus = defensiveBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.defensiveBonus);
            IncFlatArmorMod(owner, this.defensiveBonus);
            SpellEffectCreate(out this.taric, out _, "maokai_elementalAdvance_armor.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CENTER_LOC", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.taric);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.defensiveBonus);
        }
    }
}