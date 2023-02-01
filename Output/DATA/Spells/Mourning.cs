#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Mourning : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "bleeding_GLB2_tar.troy", },
            BuffName = "Mourning",
            BuffTextureName = "3069_Sword_of_Light_and_Shadow.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, 4, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, 1, false, false, attacker);
            }
        }
    }
}
namespace Spells
{
    public class Mourning : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Particle part1; // UNUSED
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            SpellEffectCreate(out part1, out _, "executionersCalling_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false);
            SpellEffectCreate(out part1, out _, "executionersCalling_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "head", default, target, default, default, false);
            AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name1 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name2 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name3 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name4 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name5 == nameof(Spells.Mourning))
            {
                SetSlotSpellCooldownTimeVer2(20, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
        }
    }
}