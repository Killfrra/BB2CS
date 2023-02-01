#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EnrageDeathRecapFix : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon_tip", "", "", },
            AutoBuffActivateEffect = new[]{ "Enrageweapon_buf.troy", "", "", },
            BuffName = "Enrage",
            BuffTextureName = "Sion_SpiritRage.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float damageToDeal;
            damageToDeal = 0 + damageAmount;
            SpellBuffClear(owner, nameof(Buffs.EnrageDeathRecapFix));
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            damageAmount -= damageAmount;
        }
    }
}