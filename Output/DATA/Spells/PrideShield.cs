#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class PrideShield : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float aP;
            float aPBonus;
            float shieldHealth;
            float nextBuffVars_ShieldHealth;
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            aP = GetFlatMagicDamageMod(owner);
            aPBonus = aP * 1.5f;
            shieldHealth = aPBonus + 200;
            nextBuffVars_ShieldHealth = shieldHealth;
            AddBuff((ObjAIBase)owner, owner, new Buffs.PrideShield(nextBuffVars_ShieldHealth), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 1);
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name1 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name2 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name3 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name4 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name5 == nameof(Spells.PrideShield))
            {
                SetSlotSpellCooldownTimeVer2(45, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
        }
    }
}
namespace Buffs
{
    public class PrideShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "RazzlesPride",
            BuffTextureName = "082_Rune_of_Rebirth.dds",
        };
        float shieldHealth;
        float initialHealth;
        bool _100Destroyed;
        bool _66Destroyed;
        Particle particle1;
        Particle particle2;
        Particle particle3;
        public PrideShield(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldHealth);
            this.initialHealth = this.shieldHealth;
            this._100Destroyed = false;
            this._66Destroyed = false;
            SpellEffectCreate(out this.particle1, out _, "razzlespride_100.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SpellEffectCreate(out this.particle2, out _, "razzlespride_66.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SpellEffectCreate(out this.particle3, out _, "razzlespride_33.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SetBuffToolTipVar(1, this.shieldHealth);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle3);
            if(!this._66Destroyed)
            {
                SpellEffectRemove(this.particle2);
            }
            if(!this._100Destroyed)
            {
                SpellEffectRemove(this.particle1);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(this.shieldHealth >= damageAmount)
            {
                float percentRemain;
                this.shieldHealth -= damageAmount;
                damageAmount = 0;
                SetBuffToolTipVar(1, this.shieldHealth);
                percentRemain = this.shieldHealth / this.initialHealth;
                if(!this._66Destroyed)
                {
                    if(percentRemain <= 0.33f)
                    {
                        this._66Destroyed = true;
                        SpellEffectRemove(this.particle2);
                    }
                }
                if(!this._100Destroyed)
                {
                    if(percentRemain <= 0.66f)
                    {
                        SpellEffectRemove(this.particle1);
                        this._100Destroyed = true;
                    }
                }
            }
            else
            {
                damageAmount -= this.shieldHealth;
                this.shieldHealth = 0;
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}