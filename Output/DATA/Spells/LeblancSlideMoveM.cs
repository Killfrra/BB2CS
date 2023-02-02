#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeblancSlideMoveM : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        Vector3 ownerPos;
        Vector3 castPosition;
        float aEDamage;
        float distance;
        Particle b;
        Particle distortionFx;
        Particle partname; // UNUSED
        int[] effect0 = {22, 44, 66, 88, 110};
        int[] effect1 = {25, 50, 75, 100, 125};
        int[] effect2 = {28, 56, 84, 112, 140};
        int[] effect3 = {20, 40, 60, 80, 100};
        public LeblancSlideMoveM(Vector3 ownerPos = default, Vector3 castPosition = default, float aEDamage = default, float distance = default)
        {
            this.ownerPos = ownerPos;
            this.castPosition = castPosition;
            this.aEDamage = aEDamage;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            Vector3 ownerPos; // UNUSED
            float distance;
            TeamId casterID; // UNITIALIZED
            //RequireVar(this.aEDamage);
            //RequireVar(this.silenceDuration);
            //RequireVar(this.ownerPos);
            //RequireVar(this.castPosition);
            ownerPos = this.ownerPos;
            distance = DistanceBetweenObjectAndPoint(owner, this.castPosition);
            if(distance < 10)
            {
                Vector3 castPosition; // UNUSED
                castPosition = this.castPosition;
                castPosition = GetPointByUnitFacingOffset(owner, 10, 0);
            }
            Move(owner, this.castPosition, 1600, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            PlayAnimation("Spell2", 0, owner, true, false, true);
            SpellEffectCreate(out this.b, out _, "Leblanc_displacement_blink_target_ult.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.distortionFx, out _, "LeBlanc_Displacement_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.b);
            UnlockAnimation(owner, true);
            SpellEffectRemove(this.distortionFx);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            Vector3 currentPosition; // UNUSED
            int level;
            TeamId casterID;
            currentPosition = GetUnitPosition(owner);
            SpellEffectCreate(out this.partname, out _, "leBlanc_slide_impact_self_ult.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            casterID = GetTeamID(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle aoehit; // UNUSED
                BreakSpellShields(unit);
                SpellEffectCreate(out aoehit, out _, "leBlanc_slide_impact_unit_tar.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                if(level == 1)
                {
                    ApplyDamage((ObjAIBase)owner, unit, this.aEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.66f, 1, false, false, (ObjAIBase)owner);
                }
                else if(level == 2)
                {
                    ApplyDamage((ObjAIBase)owner, unit, this.aEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, (ObjAIBase)owner);
                }
                else
                {
                    ApplyDamage((ObjAIBase)owner, unit, this.aEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.84f, 1, false, false, (ObjAIBase)owner);
                }
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.LeblancChaosOrbM)) > 0)
                {
                    ApplySilence(attacker, unit, 2);
                    SpellBuffRemove(unit, nameof(Buffs.LeblancChaosOrbM), (ObjAIBase)owner, 0);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level == 1)
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.33f, 1, false, false, attacker);
                    }
                    else if(level == 2)
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, unit, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 1, false, false, attacker);
                    }
                    else
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, unit, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.42f, 1, false, false, attacker);
                    }
                }
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.LeblancChaosOrb)) > 0)
                {
                    ApplySilence(attacker, unit, 2);
                    SpellBuffRemove(unit, nameof(Buffs.LeblancChaosOrb), (ObjAIBase)owner, 0);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, unit, this.effect3[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 1, false, false, attacker);
                }
            }
        }
    }
}