#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeblancSlideMove : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        float aEDamage;
        Vector3 ownerPos;
        Vector3 castPosition;
        float distance;
        Particle b;
        Particle distortionFx;
        Particle partname; // UNUSED
        int[] effect0 = {20, 40, 60, 80, 100};
        int[] effect1 = {22, 44, 66, 88, 110};
        int[] effect2 = {25, 50, 75, 100, 125};
        int[] effect3 = {28, 56, 84, 112, 140};
        public LeblancSlideMove(float aEDamage = default, Vector3 ownerPos = default, Vector3 castPosition = default, float distance = default)
        {
            this.aEDamage = aEDamage;
            this.ownerPos = ownerPos;
            this.castPosition = castPosition;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            Vector3 ownerPos; // UNUSED
            float distance;
            Vector3 castPosition; // UNUSED
            TeamId casterID; // UNITIALIZED
            //RequireVar(this.aEDamage);
            //RequireVar(this.silenceDuration);
            //RequireVar(this.baseCooldown);
            //RequireVar(this.castPosition);
            //RequireVar(this.ownerPos);
            ownerPos = this.ownerPos;
            distance = DistanceBetweenObjectAndPoint(owner, this.castPosition);
            if(distance < 10)
            {
                castPosition = this.castPosition;
                castPosition = GetPointByUnitFacingOffset(owner, 10, 0);
            }
            Move(owner, this.castPosition, 1600, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            PlayAnimation("Spell2", 0, owner, true, false, true);
            SpellEffectCreate(out this.b, out _, "Leblanc_displacement_blink_target.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.distortionFx, out _, "LeBlanc_Displacement_Yellow_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            SpellEffectRemove(this.distortionFx);
            SpellEffectRemove(this.b);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            Vector3 currentPosition; // UNUSED
            TeamId casterID;
            Particle aoehit; // UNUSED
            int level;
            SpellEffectCreate(out this.partname, out _, "leBlanc_slide_impact_self.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            currentPosition = GetUnitPosition(owner);
            casterID = GetTeamID(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                SpellEffectCreate(out aoehit, out _, "leBlanc_slide_impact_unit_tar.troy", default, casterID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                ApplyDamage((ObjAIBase)owner, unit, this.aEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, (ObjAIBase)owner);
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.LeblancChaosOrb)) > 0)
                {
                    ApplySilence(attacker, unit, 2);
                    SpellBuffRemove(unit, nameof(Buffs.LeblancChaosOrb), (ObjAIBase)owner, 0);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 1, false, false, attacker);
                }
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.LeblancChaosOrbM)) > 0)
                {
                    ApplySilence(attacker, unit, 2);
                    SpellBuffRemove(unit, nameof(Buffs.LeblancChaosOrbM), (ObjAIBase)owner, 0);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level == 1)
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, unit, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.33f, 1, false, false, attacker);
                    }
                    else if(level == 2)
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, unit, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 1, false, false, attacker);
                    }
                    else
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, unit, this.effect3[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.42f, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}