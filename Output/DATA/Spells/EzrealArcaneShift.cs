#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class EzrealArcaneShift : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Region bubbleID; // UNUSED
        public override void SelfExecute()
        {
            int ownerSkinID;
            Vector3 castPos;
            Vector3 ownerPos;
            float distance;
            TeamId teamID;
            Particle p3; // UNUSED
            Particle ar1; // UNUSED
            TeamId casterID;
            bool fired;
            bool isStealthed;
            bool canSee;
            ownerSkinID = GetSkinID(owner);
            castPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, castPos);
            FaceDirection(owner, castPos);
            if(distance >= 475)
            {
                castPos = GetPointByUnitFacingOffset(owner, 475, 0);
            }
            TeleportToPosition(owner, castPos);
            teamID = GetTeamID(owner);
            if(ownerSkinID == 5)
            {
                if(teamID == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out p3, out _, "Ezreal_arcaneshift_cas_pulsefire.troy", default, TeamId.TEAM_BLUE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                    SpellEffectCreate(out ar1, out _, "Ezreal_arcaneshift_flash_pulsefire.troy", default, TeamId.TEAM_BLUE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out p3, out _, "Ezreal_arcaneshift_cas_pulsefire.troy", default, TeamId.TEAM_PURPLE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                    SpellEffectCreate(out ar1, out _, "Ezreal_arcaneshift_flash_pulsefire.troy", default, TeamId.TEAM_PURPLE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
            }
            else
            {
                if(teamID == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out p3, out _, "Ezreal_arcaneshift_cas.troy", default, TeamId.TEAM_BLUE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                    SpellEffectCreate(out ar1, out _, "Ezreal_arcaneshift_flash.troy", default, TeamId.TEAM_BLUE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out p3, out _, "Ezreal_arcaneshift_cas.troy", default, TeamId.TEAM_PURPLE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
                    SpellEffectCreate(out ar1, out _, "Ezreal_arcaneshift_flash.troy", default, TeamId.TEAM_PURPLE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
            }
            casterID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            fired = false;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 750, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 5, default, true))
            {
                if(!fired)
                {
                    isStealthed = GetStealthed(unit);
                    canSee = CanSeeTarget(owner, unit);
                    if(!isStealthed)
                    {
                        this.bubbleID = AddUnitPerceptionBubble(casterID, 100, unit, 1, default, default, false);
                        FaceDirection(attacker, unit.Position);
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                        fired = true;
                    }
                    else if(unit is Champion)
                    {
                        this.bubbleID = AddUnitPerceptionBubble(casterID, 100, unit, 1, default, default, false);
                        FaceDirection(attacker, unit.Position);
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                        fired = true;
                    }
                    else if(canSee)
                    {
                        this.bubbleID = AddUnitPerceptionBubble(casterID, 100, unit, 1, default, default, false);
                        FaceDirection(attacker, unit.Position);
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                        fired = true;
                    }
                }
            }
        }
    }
}