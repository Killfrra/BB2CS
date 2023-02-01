#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaPowerChordDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaPowerChordDebuff",
            BuffTextureName = "Sona_PowerChordCharged.dds",
        };
        Particle b;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.b, out _, "SonaPowerChord_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.b);
        }
        public override void OnDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= 0.8f;
            }
        }
    }
}