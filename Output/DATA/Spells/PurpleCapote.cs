#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class PurpleCapote : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            float nextBuffVars_ShieldHealth;
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name1 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name2 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name3 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name4 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name5 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            nextBuffVars_ShieldHealth = 400;
            AddBuff((ObjAIBase)owner, owner, new Buffs.PurpleCapote(nextBuffVars_ShieldHealth), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class PurpleCapote : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "PurpleCapote.troy", },
            BuffName = "OdinDebacleCloak",
            BuffTextureName = "3180_DebacleVeil.dds",
        };
        float shieldHealth;
        float oldArmorAmount;
        public PurpleCapote(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldHealth);
            IncreaseShield(owner, this.shieldHealth, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.shieldHealth > 0)
            {
                RemoveShield(owner, this.shieldHealth, true, false);
            }
        }
        public override void OnUpdateActions()
        {
            if(this.shieldHealth <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.shieldHealth;
            if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                if(this.shieldHealth >= damageAmount)
                {
                    this.shieldHealth -= damageAmount;
                    damageAmount = 0;
                    this.oldArmorAmount -= this.shieldHealth;
                    ReduceShield(owner, this.oldArmorAmount, true, false);
                }
                else
                {
                    damageAmount -= this.shieldHealth;
                    this.shieldHealth = 0;
                    ReduceShield(owner, this.oldArmorAmount, true, false);
                }
            }
        }
    }
}