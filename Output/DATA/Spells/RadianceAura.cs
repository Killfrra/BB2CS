#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RadianceAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "RadianceAura",
            BuffTextureName = "GemKnight_Radiance.dds",
        };
        float damageIncrease;
        float abilityPower;
        Particle particl3;
        public RadianceAura(float damageIncrease = default, float abilityPower = default)
        {
            this.damageIncrease = damageIncrease;
            this.abilityPower = abilityPower;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageIncrease);
            //RequireVar(this.abilityPower);
            if(owner is not Champion)
            {
                this.damageIncrease /= 3;
                this.abilityPower = 0;
            }
            if(owner is Champion)
            {
                TeamId teamOfOwner;
                teamOfOwner = GetTeamID(owner);
                SpellEffectCreate(out this.particl3, out _, "Taric_GemStorm_Aura.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(owner is Champion)
            {
                SpellEffectRemove(this.particl3);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageIncrease);
            IncFlatMagicDamageMod(owner, this.abilityPower);
        }
    }
}