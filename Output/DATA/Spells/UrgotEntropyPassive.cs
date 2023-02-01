#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrgotEntropyPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "UrgotEntropyDebuff",
            BuffTextureName = "Urgot_Passive.dds",
        };
        Particle particle1;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle1, out _, "UrgotEntropy_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= 0.85f;
            }
        }
    }
}