#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniArcticAssault : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SejuaniArcticAssault",
            BuffTextureName = "Sejuani_ArcticAssault.dds",
        };
        float damageToDeal;
        float dashSpeed;
        Vector3 targetPos;
        Particle a;
        Particle partname; // UNUSED
        public SejuaniArcticAssault(float damageToDeal = default, float dashSpeed = default, Vector3 targetPos = default)
        {
            this.damageToDeal = damageToDeal;
            this.dashSpeed = dashSpeed;
            this.targetPos = targetPos;
        }
        public override void OnCollision()
        {
            TeamId teamID;
            Particle asdf1; // UNUSED
            Particle asdf2; // UNUSED
            if(owner.Team != target.Team)
            {
                if(target is ObjAIBase)
                {
                    if(!target.IsDead)
                    {
                        if(GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) == 0)
                        {
                            if(target is Champion)
                            {
                                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                                UnlockAnimation(owner, true);
                                StopMoveBlock(owner);
                                IssueOrder(owner, OrderType.AttackTo, default, target);
                            }
                            else if(target is Clone)
                            {
                                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                                UnlockAnimation(owner, true);
                                StopMoveBlock(owner);
                                IssueOrder(owner, OrderType.AttackTo, default, target);
                            }
                            else if(target is not BaseTurret)
                            {
                                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniArcticAssaultMarker)) == 0)
                                {
                                    teamID = GetTeamID(owner);
                                    AddBuff((ObjAIBase)owner, target, new Buffs.SejuaniArcticAssaultMinion(), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                                    AddBuff((ObjAIBase)owner, target, new Buffs.SejuaniArcticAssaultMarker(), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                    BreakSpellShields(target);
                                    SpellEffectCreate(out asdf1, out _, "sejuani_arctic_assault_unit_tar_02.troy", default, teamID, 10, 10, TeamId.TEAM_UNKNOWN, default, owner, false, target, "C_BUFFBONE_GLB_CHEST_LOC", default, target, default, default, true, false, false, false, false);
                                    SpellEffectCreate(out asdf2, out _, "sejuani_arctic_assault_unit_tar.troy", default, teamID, 10, 10, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                                    ApplyDamage(attacker, target, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                                    SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 targetPos;
            teamID = GetTeamID(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            PlayAnimation("Spell1", 0, owner, false, true, true);
            StartTrackingCollisions(owner, true);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.damageToDeal);
            //RequireVar(this.defenses);
            targetPos = this.targetPos;
            SpellEffectCreate(out this.a, out _, "sejuani_arctic_assault_cas_04.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            Move(target, targetPos, this.dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.a);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
        public override void OnUpdateStats()
        {
            StartTrackingCollisions(owner, true);
        }
        public override void OnUpdateActions()
        {
            int level; // UNUSED
            TeamId teamID;
            Particle asdf1; // UNUSED
            Particle asdf2; // UNUSED
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                if(GetBuffCountFromCaster(unit, default, nameof(Buffs.SharedWardBuff)) == 0)
                {
                    if(unit is Champion)
                    {
                        SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                        UnlockAnimation(owner, true);
                        StopMoveBlock(owner);
                        IssueOrder(owner, OrderType.AttackTo, default, unit);
                    }
                    else if(unit is Clone)
                    {
                        SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                        UnlockAnimation(owner, true);
                        StopMoveBlock(owner);
                        IssueOrder(owner, OrderType.AttackTo, default, unit);
                    }
                    else if(unit is not BaseTurret)
                    {
                        if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniArcticAssaultMarker)) == 0)
                        {
                            teamID = GetTeamID(owner);
                            AddBuff((ObjAIBase)owner, unit, new Buffs.SejuaniArcticAssaultMinion(), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                            AddBuff((ObjAIBase)owner, unit, new Buffs.SejuaniArcticAssaultMarker(), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(unit);
                            SpellEffectCreate(out asdf1, out _, "sejuani_arctic_assault_unit_tar_02.troy", default, teamID, 10, 10, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "C_BUFFBONE_GLB_CHEST_LOC", default, unit, default, default, true, false, false, false, false);
                            SpellEffectCreate(out asdf2, out _, "sejuani_arctic_assault_unit_tar.troy", default, teamID, 10, 10, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                            ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                            SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                        }
                    }
                }
            }
        }
        public override void OnDeath()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
        public override void OnMoveEnd()
        {
            TeamId teamID;
            int level; // UNUSED
            Particle hi; // UNUSED
            Particle asdf1; // UNUSED
            Particle asdf2; // UNUSED
            teamID = GetTeamID(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SpellEffectCreate(out hi, out _, "sejuani_arctic_assault_cas_03.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.partname, out _, "Sejuani_ArcticAssault_Impact.troy", default, teamID, 300, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            SpellBuffRemove(owner, nameof(Buffs.SejuaniArcticAssault), (ObjAIBase)owner, 0);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, attacker.Position, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniArcticAssaultMarker)) == 0)
                {
                    if(unit is Champion)
                    {
                    }
                    else if(unit is Clone)
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SejuaniArcticAssaultMinion(), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SejuaniArcticAssaultMarker(), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    BreakSpellShields(unit);
                    SpellEffectCreate(out asdf1, out _, "sejuani_arctic_assault_unit_tar_02.troy", default, teamID, 10, 10, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "C_BUFFBONE_GLB_CHEST_LOC", default, unit, default, default, true, false, false, false, false);
                    SpellEffectCreate(out asdf2, out _, "sejuani_arctic_assault_unit_tar.troy", default, teamID, 10, 10, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                    SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                }
            }
        }
        public override void OnMoveSuccess()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
        public override void OnMoveFailure()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
    }
}
namespace Spells
{
    public class SejuaniArcticAssault : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 90, 130, 170, 210};
        int[] effect1 = {20, 30, 40, 50, 60};
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
            if(!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            TeamId teamID;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            float nextBuffVars_DashSpeed;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Distance;
            float nextBuffVars_DamageToDeal;
            int nextBuffVars_Defenses;
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            charVars.OwnerPos = ownerPos;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance >= 650)
            {
                distance = 650;
                targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            }
            nextBuffVars_DashSpeed = 850;
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Distance = distance;
            nextBuffVars_DamageToDeal = this.effect0[level];
            nextBuffVars_Defenses = this.effect1[level];
            AddBuff(attacker, owner, new Buffs.SejuaniArcticAssault(nextBuffVars_DamageToDeal, nextBuffVars_DashSpeed, nextBuffVars_TargetPos), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.1f, true, false, false);
            SpellEffectCreate(out _, out _, "sejuani_arctic_assault_cas_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
        }
    }
}