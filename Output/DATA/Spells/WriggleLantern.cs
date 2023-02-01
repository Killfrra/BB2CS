#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WriggleLantern : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "WriggleLantern",
            BuffTextureName = "3154_WriggleLantern.dds",
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(RandomChance() < 0.2f)
                {
                    if(target is not BaseTurret)
                    {
                        if(target is not Champion)
                        {
                            if(GetBuffCountFromCaster(target, default, nameof(Buffs.OdinGolemBombBuff)) == 0)
                            {
                                ApplyDamage(attacker, target, 425, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class WriggleLantern : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Minion other3;
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            float newCooldown;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            other3 = SpawnMinion("WriggleLantern", "WriggleLantern", "idle.lua", targetPos, teamID, true, true, false, false, false, false, 0, true, false, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.SharedWardBuff(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other3, new Buffs.WriggleLanternWard(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ItemPlacementMissile(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(avatarVars.Scout)
            {
                AddBuff(attacker, other3, new Buffs.MasteryScoutBuff(), 1, 1, 180, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SetSpell((ObjAIBase)owner, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ItemPlacementMissile));
            FaceDirection(owner, targetPos);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            newCooldown = 180;
            if(name == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name1 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name2 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name3 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name4 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name5 == nameof(Spells.WriggleLantern))
            {
                SetSlotSpellCooldownTimeVer2(newCooldown, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
        }
    }
}