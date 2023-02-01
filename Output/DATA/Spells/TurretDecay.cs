#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretDecay : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Turret Decay",
            BuffTextureName = "1020_Glowing_Orb.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(60, ref this.lastTimeExecuted, false))
            {
                IncPermanentFlatArmorMod(owner, -2);
                IncPermanentFlatSpellBlockMod(owner, -2);
            }
        }
    }
}