#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RanduinsOmen : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            Particle a; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            float castArmor;
            float castMR;
            float defTotal;
            float finalSlow;
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            float nextBuffVars_AttackSpeedMod;
            SpellEffectCreate(out a, out _, "RanduinsOmen_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            nextBuffVars_MoveSpeedMod = -0.35f;
            nextBuffVars_AttackSpeedMod = -0.35f;
            castArmor = GetArmor(owner);
            castMR = GetSpellBlock(owner);
            defTotal = castArmor + castMR;
            defTotal /= 100;
            defTotal *= 0.5f;
            finalSlow = defTotal + 1;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, finalSlow, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.BlackOmen(nextBuffVars_AttackSpeedMod), 1, 1, finalSlow, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyAssistMarker(attacker, unit, 10);
            }
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.RanduinsOmen))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name1 == nameof(Spells.RanduinsOmen))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name2 == nameof(Spells.RanduinsOmen))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name3 == nameof(Spells.RanduinsOmen))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name4 == nameof(Spells.RanduinsOmen))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name5 == nameof(Spells.RanduinsOmen))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
        }
    }
}
namespace Buffs
{
    public class RanduinsOmen : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            IncPermanentPercentCooldownMod(owner, -0.05f);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentCooldownMod(owner, 0.05f);
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(RandomChance() < 0.2f)
            {
                if(attacker is BaseTurret)
                {
                }
                else
                {
                    float nextBuffVars_MoveSpeedMod;
                    float nextBuffVars_AttackSpeedMod;
                    nextBuffVars_MoveSpeedMod = -0.35f;
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                    nextBuffVars_AttackSpeedMod = -0.35f;
                    AddBuff((ObjAIBase)owner, attacker, new Buffs.Cripple(nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
    }
}