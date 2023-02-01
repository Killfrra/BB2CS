#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVCataclysm : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVCataclysm",
            BuffTextureName = "JarvanIV_Cataclysm.dds",
            SpellToggleSlot = 4,
        };
        bool hasDealtDamage;
        bool hasCreatedRing;
        Particle cataclysmSound;
        float newCd;
        int[] effect0 = {-100, -125, -150};
        int[] effect1 = {120, 105, 90, 0, 0};
        public override void OnActivate()
        {
            int level;
            float manaReduction;
            this.hasDealtDamage = false;
            this.hasCreatedRing = false;
            SetCanCast(owner, false);
            SpellEffectCreate(out this.cataclysmSound, out _, "JarvanCataclysm_sound.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            manaReduction = this.effect0[level];
            this.newCd = this.effect1[level];
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, manaReduction, PrimaryAbilityResourceType.MANA);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SetCanCast(owner, true);
            SpellEffectRemove(this.cataclysmSound);
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.newCd;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Target, owner);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateActions()
        {
            float distance;
            if(!this.hasDealtDamage)
            {
                distance = DistanceBetweenObjects("Owner", "Attacker");
                if(distance <= 500)
                {
                    SetCanCast(owner, true);
                    this.hasDealtDamage = true;
                    SpellCast((ObjAIBase)owner, attacker, attacker.Position, attacker.Position, 1, SpellSlotType.ExtraSlots, 1, true, false, false, false, false, false);
                }
            }
        }
        public override void OnMoveSuccess()
        {
            Vector3 centerPos;
            TeamId teamID;
            Particle groundhit; // UNUSED
            Minion other2;
            Vector3 nextBuffVars_TargetPos;
            float pushDistance;
            Vector3 targetPos;
            Vector3 unitPos;
            Vector3 ownerPos;
            float distance;
            if(!this.hasCreatedRing)
            {
                centerPos = GetUnitPosition(owner);
                teamID = GetTeamID(owner);
                SpellEffectCreate(out groundhit, out _, "JarvanCataclysm_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weapon_C", default, target, default, default, false, false, false, false, false);
                foreach(Vector3 pos in GetPointsAroundCircle(centerPos, 350, 12))
                {
                    other2 = SpawnMinion("JarvanIVWall", "JarvanIVWall", "idle.lua", pos, teamID, true, true, true, true, true, true, 0, false, false, (Champion)owner);
                    FaceDirection(other2, centerPos);
                    AddBuff(other2, owner, new Buffs.JarvanIVCataclysmAttack(), 50, 1, 3.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, other2.Position, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, false))
                    {
                        if(unit is Champion)
                        {
                            pushDistance = 110;
                        }
                        else
                        {
                            pushDistance = 125;
                        }
                        if(IsInFront(other2, unit))
                        {
                            targetPos = GetPointByUnitFacingOffset(other2, pushDistance, 0);
                        }
                        else
                        {
                            unitPos = GetUnitPosition(unit);
                            ownerPos = GetUnitPosition(other2);
                            distance = DistanceBetweenPoints(unitPos, ownerPos);
                            if(distance <= 60)
                            {
                                targetPos = GetPointByUnitFacingOffset(other2, pushDistance, 0);
                            }
                            else
                            {
                                targetPos = GetPointByUnitFacingOffset(other2, pushDistance, 180);
                            }
                        }
                        nextBuffVars_TargetPos = targetPos;
                        AddBuff(other2, unit, new Buffs.GlobalWallPush(nextBuffVars_TargetPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        if(attacker.Team != unit.Team)
                        {
                            ApplyDamage(attacker, unit, 0, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 0, 0, 1, false, false, attacker);
                        }
                    }
                }
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(unit is Champion)
                    {
                        ForceRefreshPath(unit);
                    }
                }
                this.hasCreatedRing = true;
                SpellBuffClear(owner, nameof(Buffs.UnstoppableForceMarker));
            }
        }
    }
}
namespace Spells
{
    public class JarvanIVCataclysm : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
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
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            float distance; // UNUSED
            Vector3 targetPos;
            count = GetBuffCountFromAll(owner, nameof(Buffs.JarvanIVCataclysm));
            if(count >= 1)
            {
                SpellBuffClear(owner, nameof(Buffs.JarvanIVCataclysm));
                SpellBuffClear(owner, nameof(Buffs.JarvanIVCataclysmAttack));
            }
            else
            {
                AddBuff((ObjAIBase)target, owner, new Buffs.JarvanIVCataclysm(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.UnstoppableForceMarker(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, target, new Buffs.JarvanIVCataclysmVisibility(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.JarvanIVCataclysmSound(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                distance = DistanceBetweenObjects("Attacker", "Target");
                targetPos = GetUnitPosition(target);
                Move(owner, targetPos, 2000, 150, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 700, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
                SetSlotSpellCooldownTimeVer2(1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
        }
    }
}