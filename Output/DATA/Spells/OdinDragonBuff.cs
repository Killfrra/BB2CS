#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinDragonBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinDragonBuff",
            BuffTextureName = "Averdrian_AstralBeam.dds",
        };
        float damageIncMod;
        Particle buffParticle;
        float aPIncMod;
        public override void OnActivate()
        {
            this.damageIncMod = 40;
            SpellEffectCreate(out this.buffParticle, out _, "nashor_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            this.aPIncMod = 40;
            IncFlatPhysicalDamageMod(owner, this.damageIncMod);
            IncFlatMagicDamageMod(owner, this.aPIncMod);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageIncMod);
            IncFlatMagicDamageMod(owner, this.aPIncMod);
        }
    }
}