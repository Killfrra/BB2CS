#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JannaEoTSBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JannaEoTSBuff",
            BuffTextureName = "WaterWizard_Vortex.dds",
        };
        float damageBonus;
        public JannaEoTSBuff(float damageBonus = default)
        {
            this.damageBonus = damageBonus;
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageBonus);
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBonus);
        }
    }
}