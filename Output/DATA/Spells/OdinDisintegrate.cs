#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinDisintegrate : BBBuffScript
    {
    }
}
namespace Spells
{
    public class OdinDisintegrate : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float cooldownTotal;
            float targetMaxHealth;
            float damage;
            cooldownTotal = 1;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownTotal);
            targetMaxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            damage = targetMaxHealth * 0.0525f;
            ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_RAW, 1, 0, 0, true, true, attacker);
        }
    }
}