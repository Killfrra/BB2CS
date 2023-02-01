#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Bushwhack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
        };
        TeamId teamID; // UNUSED
        bool active;
        bool sprung;
        Particle particle;
        Particle emptyparticle;
        float lastTimeExecuted;
        float[] effect0 = {20, 31.25f, 42.5f, 53.75f, 65};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            SetGhosted(owner, true);
            SetInvulnerable(owner, true);
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            this.teamID = GetTeamID(owner);
            this.active = false;
            this.sprung = false;
            SpellEffectCreate(out _, out _, "nidalee_bushwhack_set_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.emptyparticle);
            ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -1);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID;
            Particle particle; // UNUSED
            int level;
            int nextBuffVars_DOTCounter;
            float nextBuffVars_DamagePerTick;
            float nextBuffVars_Debuff;
            teamID = GetTeamID(attacker);
            if(this.active)
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    teamID = GetTeamID(attacker);
                    SpellEffectCreate(out particle, out _, "nidalee_bushwhack_trigger_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out particle, out _, "nidalee_bushwhack_trigger_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, false, false, false, false);
                    level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_DOTCounter = 0;
                    nextBuffVars_DamagePerTick = this.effect0[level];
                    nextBuffVars_Debuff = this.effect1[level];
                    AddBuff(attacker, unit, new Buffs.BushwhackDebuff(nextBuffVars_Debuff), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
                    AddBuff(attacker, unit, new Buffs.BushwhackDamage(nextBuffVars_DOTCounter, nextBuffVars_DamagePerTick), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    this.sprung = true;
                }
                if(this.sprung)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
            else
            {
                if(ExecutePeriodically(0.9f, ref this.lastTimeExecuted, false))
                {
                    this.active = true;
                    SpellEffectCreate(out this.particle, out this.emptyparticle, "nidalee_trap_team_id_green.troy", "empty.troy", teamID, 0, 0, TeamId.TEAM_UNKNOWN, teamID, default, false, owner, default, default, target, default, default, false, false, false, false, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class Bushwhack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamID;
            Minion other3;
            targetPos = GetCastSpellTargetPos();
            teamID = GetTeamID(owner);
            other3 = SpawnMinion("Noxious Trap", "Nidalee_Spear", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            PlayAnimation("Spell1", 1, other3, false, false, true);
            AddBuff(attacker, other3, new Buffs.Bushwhack(), 1, 1, 240, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}