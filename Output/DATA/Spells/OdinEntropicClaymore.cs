#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OdinEntropicClaymore : BBSpellScript
    {
        public override void SelfExecute()
        {
            Particle ar; // UNUSED
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            SpellEffectCreate(out ar, out _, "spectral_fury_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinEntropicClaymore(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.OdinEntropicClaymore))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name1 == nameof(Spells.OdinEntropicClaymore))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name2 == nameof(Spells.OdinEntropicClaymore))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name3 == nameof(Spells.OdinEntropicClaymore))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name4 == nameof(Spells.OdinEntropicClaymore))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name5 == nameof(Spells.OdinEntropicClaymore))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
        }
    }
}
namespace Buffs
{
    public class OdinEntropicClaymore : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinEntropicClaymore",
            BuffTextureName = "3184_FrozenWarhammer.dds",
        };
        Particle buffParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(owner is Champion)
                    {
                        if(target is ObjAIBase)
                        {
                            if(target is BaseTurret)
                            {
                            }
                            else
                            {
                                float nextBuffVars_TickDamage;
                                float nextBuffVars_attackSpeedMod;
                                nextBuffVars_TickDamage = 40;
                                nextBuffVars_attackSpeedMod = 0;
                                AddBuff(attacker, target, new Buffs.EntropyBurning(nextBuffVars_TickDamage, nextBuffVars_attackSpeedMod), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 1, true, false, false);
                                AddBuff((ObjAIBase)target, target, new Buffs.Internal_30Slow(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                AddBuff((ObjAIBase)owner, target, new Buffs.ItemSlow(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}