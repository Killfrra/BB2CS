#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdynsVeil : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdynsVeil",
            BuffTextureName = "3180_OdynsVeil.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float oldStoredAmount; // UNUSED
        public override void OnActivate()
        {
            charVars.StoredDamage = 0;
            SetBuffToolTipVar(1, charVars.StoredDamage);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float damageReduction;
            this.oldStoredAmount = charVars.StoredDamage;
            if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                if(damageAmount > 0)
                {
                    damageReduction = damageAmount * 0.1f;
                    damageAmount *= 0.9f;
                    charVars.StoredDamage += damageReduction;
                }
            }
            charVars.StoredDamage = Math.Min(charVars.StoredDamage, 200);
            SetBuffToolTipVar(1, charVars.StoredDamage);
        }
    }
}
namespace Spells
{
    public class OdynsVeil : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            Particle a; // UNUSED
            float finalDamage;
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            SpellEffectCreate(out a, out _, "OdynsVeil_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            finalDamage = 200 + charVars.StoredDamage;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            charVars.StoredDamage = 0;
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name1 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name2 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name3 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name4 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name5 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
        }
    }
}