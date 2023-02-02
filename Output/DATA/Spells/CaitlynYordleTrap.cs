#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CaitlynYordleTrap : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {3, 3, 3, 3, 3};
        public override void SelfExecute()
        {
            int maxStacks;
            float numFound;
            float minDuration;
            AttackableUnit other2;
            Vector3 targetPos;
            TeamId teamID;
            Minion other3;
            maxStacks = this.effect0[level];
            numFound = 0;
            minDuration = 240;
            other2 = SetUnit(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectUntargetable, nameof(Buffs.CaitlynYordleTrap), true))
            {
                float durationRemaining;
                numFound++;
                durationRemaining = GetBuffRemainingDuration(unit, nameof(Buffs.CaitlynYordleTrap));
                if(durationRemaining < minDuration)
                {
                    minDuration = durationRemaining;
                    InvalidateUnit(other2);
                    other2 = SetUnit(unit);
                }
            }
            if(numFound >= maxStacks)
            {
                if(owner != other2)
                {
                    ApplyDamage((ObjAIBase)other2, other2, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)other2);
                }
            }
            targetPos = GetCastSpellTargetPos();
            teamID = GetTeamID(owner);
            other3 = SpawnMinion("Noxious Trap", "CaitlynTrap", "idle.lua", targetPos, teamID ?? TeamId.TEAM_UNKNOWN, false, true, false, true, true, false, 0, false, false, (Champion)owner);
            PlayAnimation("Spell1", 1, other3, false, false, true);
            AddBuff(attacker, other3, new Buffs.CaitlynYordleTrap(), 1, 1, 240, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class CaitlynYordleTrap : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "caitlyn_yordleTrap_set.troy", },
            BuffName = "",
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
        };
        TeamId teamID;
        bool active;
        bool sprung;
        Particle particle2;
        Particle particle;
        float lastTimeExecuted2;
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            teamID = GetTeamID(attacker);
            SetGhosted(owner, true);
            SetInvulnerable(owner, true);
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            this.teamID = GetTeamID(owner);
            this.active = false;
            this.sprung = false;
            SpellEffectCreate(out this.particle2, out this.particle, "caitlyn_yordleTrap_idle_green.troy", "caitlyn_yordleTrap_idle_red.troy", this.teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId attackerID;
            Particle asdadsfa; // UNUSED
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
            attackerID = GetTeamID(attacker);
            SpellEffectCreate(out asdadsfa, out _, "caitlyn_yordleTrap_trigger_sound.troy", default, attackerID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -1);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            if(this.active)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 135, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    Particle particle; // UNUSED
                    int level; // UNUSED
                    BreakSpellShields(unit);
                    teamID = GetTeamID(attacker);
                    SpellEffectCreate(out particle, out _, "caitlyn_yordleTrap_trigger_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, default, default, false, false);
                    level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    AddBuff(attacker, unit, new Buffs.CaitlynYordleTrapDebuff(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false, false);
                    AddBuff(attacker, unit, new Buffs.CaitlynYordleTrapSight(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    this.sprung = true;
                }
                if(this.sprung)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
            else
            {
                if(ExecutePeriodically(1, ref this.lastTimeExecuted2, false))
                {
                    this.active = true;
                }
            }
        }
    }
}