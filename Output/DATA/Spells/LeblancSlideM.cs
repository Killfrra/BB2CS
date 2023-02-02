#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LeblancSlideM : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        float[] effect0 = {1.5f, 1.75f, 2, 2.25f, 2.5f};
        float[] effect1 = {93.5f, 137.5f, 181.5f, 225.5f, 269.5f};
        float[] effect2 = {106.25f, 156.25f, 206.25f, 256.25f, 306.25f};
        int[] effect3 = {119, 175, 231, 287, 343};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            else if(!canCast)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 ownerPos;
            Vector3 castPosition;
            TeamId casterID;
            Particle smokeBomb1; // UNUSED
            float distance;
            float nextBuffVars_SilenceDuration; // UNUSED
            Vector3 nextBuffVars_OwnerPos;
            Vector3 nextBuffVars_CastPosition;
            float nextBuffVars_AEDamage;
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            ownerPos = GetUnitPosition(owner);
            castPosition = GetCastSpellTargetPos();
            casterID = GetTeamID(owner);
            SpellEffectCreate(out smokeBomb1, out _, "leBlanc_displacement_cas_ult.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false);
            distance = DistanceBetweenPoints(ownerPos, castPosition);
            if(distance > 600)
            {
                FaceDirection(owner, castPosition);
                castPosition = GetPointByUnitFacingOffset(owner, 600, 0);
            }
            nextBuffVars_SilenceDuration = this.effect0[level];
            nextBuffVars_OwnerPos = ownerPos;
            nextBuffVars_CastPosition = castPosition;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_AEDamage = this.effect1[level];
            }
            else if(level == 2)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_AEDamage = this.effect2[level];
            }
            else
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_AEDamage = this.effect3[level];
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeblancSlideM(nextBuffVars_OwnerPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeblancSlideMoveM(nextBuffVars_OwnerPos, nextBuffVars_CastPosition, nextBuffVars_AEDamage), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeblancSlideWallFixM(), 1, 1, 3.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LeblancSlideM : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancDisplacementM",
            BuffTextureName = "LeblancDisplacementReturnM.dds",
            SpellToggleSlot = 4,
        };
        Vector3 ownerPos;
        bool doNotTeleport;
        Particle yellowIndicator;
        public LeblancSlideM(Vector3 ownerPos = default)
        {
            this.ownerPos = ownerPos;
        }
        public override void OnActivate()
        {
            Vector3 ownerPos;
            TeamId casterID;
            //RequireVar(this.ownerPos);
            this.doNotTeleport = false;
            ownerPos = this.ownerPos;
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSlideReturnM));
            casterID = GetTeamID(owner);
            if(casterID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.yellowIndicator, out _, "Leblanc_displacement_blink_indicator_ult.troy", default, TeamId.TEAM_BLUE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, owner, default, default, false, default, default, false);
            }
            else
            {
                SpellEffectCreate(out this.yellowIndicator, out _, "Leblanc_displacement_blink_indicator_ult.troy", default, TeamId.TEAM_PURPLE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, owner, default, default, false, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.yellowIndicator);
            if(!this.doNotTeleport)
            {
                if(!expired)
                {
                    Vector3 ownerPos;
                    Vector3 currentPosition;
                    TeamId casterID;
                    Particle smokeBomb1; // UNUSED
                    Particle smokeBomb2; // UNUSED
                    Particle a; // UNUSED
                    ownerPos = this.ownerPos;
                    currentPosition = GetUnitPosition(owner);
                    casterID = GetTeamID(owner);
                    SpellEffectCreate(out smokeBomb1, out _, "leBlanc_displacement_cas.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, currentPosition, target, default, default, true, default, default, false);
                    TeleportToPosition(owner, ownerPos);
                    SpellEffectCreate(out smokeBomb2, out _, "leBlanc_displacement_cas.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false);
                    SpellEffectCreate(out a, out _, "Leblanc_displacement_blink_return_trigger.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, owner, default, ownerPos, true, default, default, false);
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.LeblancSlideReturnM))
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.LeblancSlide)) > 0)
                {
                    float cooldownTime;
                    cooldownTime = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldownTime <= 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.25f);
                    }
                }
                SpellBuffRemove(owner, nameof(Buffs.LeblancSlideWallFixM), (ObjAIBase)owner);
                SpellBuffRemoveCurrent(owner);
            }
            if(spellName == nameof(Spells.LeblancSlide))
            {
                this.doNotTeleport = true;
                SpellBuffRemove(owner, nameof(Buffs.LeblancSlideWallFixM), (ObjAIBase)owner);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}