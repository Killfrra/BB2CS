#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BeaconAuraNoParticleAP : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rally AP",
            BuffTextureName = "Summoner_rally.dds",
        };
        float damageMod;
        float apMod;
        public override void OnActivate()
        {
            int ownerLevel;
            //RequireVar(this.finalHPRegen);
            ownerLevel = GetLevel(attacker);
            this.damageMod = 1.47059f * ownerLevel;
            this.damageMod += 8.5294f;
            this.apMod = this.damageMod * 2;
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageMod);
            IncFlatMagicDamageMod(owner, this.apMod);
        }
    }
}