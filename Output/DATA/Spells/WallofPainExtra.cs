#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WallofPainExtra : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Wall of Pain",
            BuffTextureName = "Lich_WallOfPain.dds",
        };
        float armorMod;
        public WallofPainExtra(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorMod);
            IncFlatSpellBlockMod(owner, this.armorMod);
        }
    }
}