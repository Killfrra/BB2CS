#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxLightstrikeDetonator : BBBuffScript
    {
        float lSCooldown;
        Vector3 attacker;
        public LuxLightstrikeDetonator(float lSCooldown = default, Vector3 attacker = default)
        {
            this.lSCooldown = lSCooldown;
            this.attacker = attacker;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lSCooldown);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LuxLightstrikeToggle));
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, this.attacker, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage((ObjAIBase)owner, unit, 300, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
            ApplyDamage(attacker, attacker, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.lSCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LuxLightstrikeKugel));
        }
    }
}