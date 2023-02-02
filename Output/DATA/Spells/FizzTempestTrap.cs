#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FizzTempestTrap : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {2, 2, 2, 2, 2};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamID;
            Minion other3;
            int maxStacks;
            targetPos = GetCastSpellTargetPos();
            teamID = GetTeamID(owner);
            other3 = SpawnMinion("Bantam Trap", "CaitlynTrap", "idle.lua", targetPos, teamID ?? TeamId.TEAM_UNKNOWN, false, true, false, true, true, false, 0, false, false, (Champion)owner);
            PlayAnimation("Spell1", 1, other3, false, false, true);
            AddBuff(attacker, other3, new Buffs.FizzTempestTrap(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            maxStacks = this.effect0[level];
            AddBuff(other3, owner, new Buffs.FizzTempestTrapCount(), maxStacks, 1, 30, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class FizzTempestTrap : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "caitlyn_yordleTrap_set.troy", },
            BuffName = "",
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
        };
        TeamId teamID; // UNUSED
        bool active;
        bool sprung; // UNUSED
        Particle particle;
        Particle particle2;
        float delay;
        float lastTimeExecuted2;
        int[] effect0 = {80, 110, 140, 170, 200};
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 spawnPos; // UNUSED
            teamID = GetTeamID(attacker);
            SetGhosted(owner, true);
            SetInvulnerable(owner, true);
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            this.teamID = GetTeamID(owner);
            this.active = false;
            this.sprung = false;
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "caitlyn_yordleTrap_idle_red.troy", default, TeamId.TEAM_BLUE, 10, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.particle2, out _, "caitlyn_yordleTrap_idle_green.troy", default, TeamId.TEAM_BLUE, 10, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "caitlyn_yordleTrap_idle_red.troy", default, TeamId.TEAM_PURPLE, 10, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, target, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.particle2, out _, "caitlyn_yordleTrap_idle_green.troy", default, TeamId.TEAM_PURPLE, 10, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, target, default, default, false, false, false, false, false);
            }
            this.delay = 1;
            spawnPos = GetPointByUnitFacingOffset(owner, 50, 180);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID1; // UNUSED
            teamID1 = GetTeamID(attacker);
            if(this.active)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 100, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.MarknFranzFranzTrapNoFling), false))
                {
                    bool moving;
                    moving = IsMoving(unit);
                    if(moving)
                    {
                        TeamId teamID2; // UNUSED
                        TeamId teamID;
                        Particle particle; // UNUSED
                        Particle asdadsfa; // UNUSED
                        int level;
                        Vector3 landPos;
                        int dmg; // UNUSED
                        teamID2 = GetTeamID(unit);
                        BreakSpellShields(unit);
                        teamID = GetTeamID(attacker);
                        SpellEffectCreate(out particle, out _, "caitlyn_yordleTrap_trigger_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, false, false, false, false);
                        SpellEffectCreate(out asdadsfa, out _, "caitlyn_yordleTrap_trigger_sound.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                        level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        landPos = GetPointByUnitFacingOffset(owner, 620, 0);
                        ApplyAssistMarker(attacker, unit, 10);
                        dmg = this.effect0[level];
                        landPos = GetPointByUnitFacingOffset(unit, 550, 0);
                        Move(unit, landPos, 650, 60, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 420, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
                        this.active = false;
                        this.delay = 4;
                        UnlockAnimation(owner, false);
                        PlayAnimation("Death", 4, owner, false, false, true);
                    }
                }
            }
            else
            {
                if(ExecutePeriodically(1, ref this.lastTimeExecuted2, false))
                {
                    this.delay--;
                    if(this.delay <= 0)
                    {
                        this.active = true;
                    }
                    else if(this.delay <= 1)
                    {
                        PlayAnimation("Spell1", 1, owner, false, false, true);
                    }
                }
            }
        }
    }
}