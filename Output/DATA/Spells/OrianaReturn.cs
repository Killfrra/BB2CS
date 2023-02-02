#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OrianaReturn : BBSpellScript
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
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.OrianaReturn));
            if(spellName == nameof(Spells.Yomureturn))
            {
                correctSpell = true;
            }
            else if(spellName == nameof(Spells.OrianaReturn))
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
                    SpellBuffClear(owner, nameof(Buffs.OrianaReturn));
                }
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, attacker, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffClear(owner, nameof(Buffs.OrianaReturn));
        }
    }
}
namespace Buffs
{
    public class OrianaReturn : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Malady",
            BuffTextureName = "3114_Malady.dds",
            SpellVOOverrideSkins = new[]{ "BroOlaf", },
        };
        public override void OnActivate()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            SetSpellOffsetTarget(3, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID; // UNUSED
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            charVars.GhostAlive = false;
            DestroyMissile(charVars.MissileID);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            teamID = GetTeamID(owner);
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            SpellMissile missileId; // UNITIALIZED
            charVars.MissileID = missileId;
            charVars.GhostAlive = true;
        }
    }
}