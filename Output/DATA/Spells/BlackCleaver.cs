#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlackCleaver : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Black Cleaver",
            BuffTextureName = "3071_The_Black_Cleaver.dds",
        };
        float armorReduction;
        public BlackCleaver(float armorReduction = default)
        {
            this.armorReduction = armorReduction;
        }
        public override void OnActivate()
        {
            Particle particle; // UNUSED
            //RequireVar(this.armorReduction);
            SpellEffectCreate(out particle, out _, "BlackCleave_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorReduction);
        }
    }
}