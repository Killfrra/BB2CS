#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OrianaDissonance : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        bool hit; // UNUSED
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            bool correctSpell;
            float duration;
            correctSpell = false;
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.OrianaDissonance));
            if(spellName == nameof(Spells.Yomudissonance))
            {
                correctSpell = true;
            }
            else if(spellName == nameof(Spells.OrianaDissonance))
            {
                correctSpell = true;
            }
            if(duration >= 0.01f)
            {
                if(correctSpell)
                {
                    this.hit = true;
                    charVars.GhostAlive = false;
                    DestroyMissile(charVars.MissileID);
                    SpellBuffClear(owner, nameof(Buffs.OrianaDissonance));
                }
            }
        }
        public override void OnMissileUpdate(SpellMissile missileNetworkID, Vector3 missilePosition)
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, missilePosition, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.OrianaShock)) == 0)
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.OrianaShock(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.OrianaGhostEnemy(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            DestroyMissile(missileNetworkID);
        }
    }
}
namespace Buffs
{
    public class OrianaDissonance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Malady",
            BuffTextureName = "3114_Malady.dds",
            SpellVOOverrideSkins = new[]{ "BroOlaf", },
        };
        bool hit; // UNUSED
        public override void OnActivate()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            this.hit = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            SpellMissile missileId; // UNITIALIZED
            charVars.MissileID = missileId;
            charVars.GhostAlive = true;
        }
    }
}