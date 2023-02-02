#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FizzJumpTwo : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 20f, 18f, 16f, 14f, 12f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {70, 115, 160, 205, 250};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            float gravityVar;
            float speedVar;
            int nextBuffVars_Damage; // UNUSED
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            if(distance >= 300)
            {
                bool result;
                gravityVar = 30;
                speedVar = 1325;
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, 300, 0);
                distance = 275;
                result = IsPathable(targetPos);
                if(!result)
                {
                    float checkDistance;
                    checkDistance = 300;
                    while(checkDistance <= 400)
                    {
                        bool pathable;
                        checkDistance += 25;
                        targetPos = GetPointByUnitFacingOffset(owner, checkDistance, 0);
                        pathable = IsPathable(targetPos);
                        if(pathable)
                        {
                            distance = checkDistance;
                            checkDistance = 500;
                        }
                    }
                }
            }
            else if(distance >= 200)
            {
                gravityVar = 25;
                speedVar = 1175;
            }
            else if(distance >= 100)
            {
                gravityVar = 20;
                speedVar = 900;
            }
            else if(distance >= 25)
            {
                gravityVar = 15;
                speedVar = 825;
            }
            else if(distance < 25)
            {
                gravityVar = 15;
                speedVar = 800;
                targetPos = GetPointByUnitFacingOffset(owner, 25, 0);
            }
            Move(owner, targetPos, speedVar, gravityVar, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 500, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            nextBuffVars_Damage = this.effect0[level];
            AddBuff(attacker, attacker, new Buffs.FizzJumpTwo(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell3d", 1, owner, false, true, false);
        }
    }
}
namespace Buffs
{
    public class FizzJumpTwo : BBBuffScript
    {
        Particle a; // UNUSED
        int failCount;
        int[] effect0 = {16, 14, 12, 10, 8};
        int[] effect1 = {70, 120, 170, 220, 270};
        int[] effect2 = {70, 120, 170, 220, 270};
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            //RequireVar(this.damage);
            SpellEffectCreate(out this.a, out _, "Fizz_Jump_cas.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.a, out _, "Fizz_Jump_WeaponGlow.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, true, false, false, false, false);
            SetCallForHelpSuppresser(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetInvulnerable(owner, false);
            SetTargetable(owner, false);
            SpellBuffClear(owner, nameof(Buffs.FizzJumpDelay));
            DestroyMissileForTarget(owner);
            this.failCount = 0;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            float cDReduction;
            int level;
            float baseCD;
            float lowerCD;
            float newCD;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SetTargetable(owner, true);
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            SetSilenced(owner, false);
            SetForceRenderParticles(owner, false);
            SetInvulnerable(owner, false);
            SetTargetable(owner, true);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzJump));
            cDReduction = GetPercentCooldownMod(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseCD = this.effect0[level];
            lowerCD = baseCD * cDReduction;
            newCD = baseCD + lowerCD;
            SetSlotSpellCooldownTimeVer2(newCD, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            UnlockAnimation(owner, true);
        }
        public override void OnMoveSuccess()
        {
            TeamId teamID;
            int level;
            Particle asdf; // UNUSED
            Particle b; // UNUSED
            teamID = GetTeamID(owner);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FizzJumpBuffered)) == 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                teamID = GetTeamID(owner);
                SpellEffectCreate(out asdf, out _, "Fizz_TrickSlamTwo.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                SetTargetable(owner, true);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, (ObjAIBase)owner);
                    SpellEffectCreate(out b, out _, "Fizz_TrickSlam_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                SpellBuffClear(owner, nameof(Buffs.FizzJumpTwo));
            }
            else if(this.failCount == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                teamID = GetTeamID(owner);
                SpellEffectCreate(out asdf, out _, "Fizz_TrickSlamTwo.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                SetTargetable(owner, true);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, (ObjAIBase)owner);
                    SpellEffectCreate(out b, out _, "Fizz_TrickSlam_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                SpellBuffClear(owner, nameof(Buffs.FizzJumpTwo));
            }
            else
            {
                this.failCount = 1;
            }
        }
        public override void OnMoveFailure()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}