#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Reinforce : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Reinforce",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        float armorMod;
        public Reinforce(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorMod);
        }
    }
}