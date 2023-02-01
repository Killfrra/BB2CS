#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaIDDamage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "corki_fire_buf.troy", },
            BuffName = "Poisoned",
            BuffTextureName = "Jester_DeathWard.dds",
        };
        float damagePerTick;
        public ShyvanaIDDamage(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            float bonusAD;
            //RequireVar(this.damagePerTick);
            bonusAD = GetFlatPhysicalDamageMod(owner);
            bonusAD *= 0.2f;
            this.damagePerTick += bonusAD;
            ApplyDamage(attacker, owner, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
    }
}