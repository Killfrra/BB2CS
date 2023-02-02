#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RallyingBanner : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Stark's Fervor",
            BuffTextureName = "3050_Rallying_Banner.dds",
        };
        float armorMod;
        public RallyingBanner(float armorMod = default)
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
            if(attacker.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                float dist;
                dist = DistanceBetweenObjects("Attacker", "Owner");
                if(dist >= 1200)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}