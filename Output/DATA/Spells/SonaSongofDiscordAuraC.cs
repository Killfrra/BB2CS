#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaSongofDiscordAuraC : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "SonaSongofDiscord_tar.troy", "", },
            BuffName = "SonaSongofDiscordAuraB",
            BuffTextureName = "Sona_SongofDiscord.dds",
        };
        float damageAura;
        float lastTimeExecuted;
        public SonaSongofDiscordAuraC(float damageAura = default)
        {
            this.damageAura = damageAura;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageAura);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, this.damageAura, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.1f, 1, false, false, attacker);
            }
        }
    }
}