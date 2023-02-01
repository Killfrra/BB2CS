#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingDoubleAttackDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MonkeyKingDoubleAttackDebuff",
            BuffTextureName = "MonkeyKingCrushingBlow.dds",
        };
        float armorDebuff;
        Particle particle1;
        public MonkeyKingDoubleAttackDebuff(float armorDebuff = default)
        {
            this.armorDebuff = armorDebuff;
        }
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            teamID = GetTeamID(owner);
            //RequireVar(this.armorDebuff);
            IncPercentArmorMod(owner, this.armorDebuff);
            SpellEffectCreate(out this.particle1, out _, "monkey_king_crushingBlow_armor_debuff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, this.armorDebuff);
        }
    }
}