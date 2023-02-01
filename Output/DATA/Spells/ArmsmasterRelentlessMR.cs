#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ArmsmasterRelentlessMR : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "JaxRelentlessAssault_buf.troy", },
            BuffName = "RelentlessBarrier",
            BuffTextureName = "Armsmaster_RelentlessMR.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, charVars.BonusMR);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            Particle onHitSpellEffect; // UNUSED
            if(damageSource != default)
            {
                if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
                {
                    SpellEffectCreate(out onHitSpellEffect, out _, "JaxRelentlessAssaultShield_hit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                }
            }
        }
    }
}