#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeblancSlide : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancDisplacement",
            BuffTextureName = "LeblancDisplacementReturn.dds",
            SpellToggleSlot = 2,
        };
        Vector3 ownerPos;
        bool doNotTeleport;
        Particle yellowIndicator;
        public LeblancSlide(Vector3 ownerPos = default)
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
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSlideReturn));
            casterID = GetTeamID(owner);
            if(casterID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.yellowIndicator, out _, "Leblanc_displacement_blink_indicator.troy", default, TeamId.TEAM_BLUE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, owner, default, default, false, default, default, false);
            }
            else
            {
                SpellEffectCreate(out this.yellowIndicator, out _, "Leblanc_displacement_blink_indicator.troy", default, TeamId.TEAM_PURPLE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, owner, default, default, false, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 ownerPos;
            Vector3 currentPosition;
            TeamId casterID;
            Particle smokeBomb1; // UNUSED
            Particle smokeBomb2; // UNUSED
            Particle a; // UNUSED
            SpellEffectRemove(this.yellowIndicator);
            if(!this.doNotTeleport)
            {
                if(!expired)
                {
                    ownerPos = this.ownerPos;
                    currentPosition = GetUnitPosition(owner);
                    casterID = GetTeamID(owner);
                    SpellEffectCreate(out smokeBomb1, out _, "leBlanc_displacement_cas.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, currentPosition, target, default, default, true, default, default, false);
                    TeleportToPosition(owner, ownerPos);
                    SpellEffectCreate(out smokeBomb2, out _, "leBlanc_displacement_cas.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false);
                    SpellEffectCreate(out a, out _, "Leblanc_displacement_blink_return_trigger.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, owner, default, ownerPos, true, default, default, false);
                }
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeblancSlideWallFix)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.LeblancSlideWallFix), (ObjAIBase)owner);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            float cooldownTime;
            spellName = GetSpellName();
            if(spellName == nameof(Spells.LeblancSlideReturn))
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.LeblancSlideM)) > 0)
                {
                    cooldownTime = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldownTime <= 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.25f);
                    }
                }
                SpellBuffRemoveCurrent(owner);
            }
            spellName = GetSpellName();
            if(spellName == nameof(Spells.LeblancSlideM))
            {
                this.doNotTeleport = true;
                SpellBuffRemoveCurrent(owner);
                SpellBuffRemove(owner, nameof(Buffs.LeblancSlideMove), (ObjAIBase)owner);
            }
        }
    }
}
namespace Spells
{
    public class LeblancSlide : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        int[] effect0 = {85, 125, 165, 205, 245};
        float[] effect1 = {1.5f, 1.75f, 2, 2.25f, 2.5f};
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
            TeamId teamOfOwner; // UNUSED
            int nextBuffVars_AEDamage;
            float nextBuffVars_SilenceDuration;
            Vector3 nextBuffVars_OwnerPos;
            Vector3 nextBuffVars_CastPosition;
            ownerPos = GetUnitPosition(owner);
            castPosition = GetCastSpellTargetPos();
            casterID = GetTeamID(owner);
            SpellEffectCreate(out smokeBomb1, out _, "leBlanc_displacement_cas.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, default, default, false);
            distance = DistanceBetweenPoints(ownerPos, castPosition);
            if(distance > 600)
            {
                FaceDirection(owner, castPosition);
                castPosition = GetPointByUnitFacingOffset(owner, 600, 0);
            }
            teamOfOwner = GetTeamID(owner);
            nextBuffVars_AEDamage = this.effect0[level];
            nextBuffVars_SilenceDuration = this.effect1[level];
            nextBuffVars_OwnerPos = ownerPos;
            nextBuffVars_CastPosition = castPosition;
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeblancSlide(nextBuffVars_OwnerPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeblancSlideMove(nextBuffVars_AEDamage, nextBuffVars_OwnerPos, nextBuffVars_CastPosition), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeblancSlideWallFix(), 1, 1, 3.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}