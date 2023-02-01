#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VladimirTransfusion : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
        };
        int[] effect0 = {0, 0, 0, 0, 0};
        int[] effect1 = {90, 125, 160, 195, 230};
        public override void SelfExecute()
        {
            float healthCost;
            float temp1;
            healthCost = this.effect0[level];
            temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 targetPos;
            targetPos = GetCastSpellTargetPos();
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
            SpellCast(attacker, owner, attacker.Position, owner.Position, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, targetPos);
        }
    }
}