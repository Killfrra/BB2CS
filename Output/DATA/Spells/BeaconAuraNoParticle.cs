#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BeaconAuraNoParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rally",
            BuffTextureName = "Summoner_rally.dds",
        };
        float damageMod;
        public override void OnActivate()
        {
            int ownerLevel;
            //RequireVar(this.finalHPRegen);
            ownerLevel = GetLevel(attacker);
            this.damageMod = 1.47059f * ownerLevel;
            this.damageMod += 8.5294f;
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageMod);
        }
    }
}