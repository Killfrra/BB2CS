#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneUnspeakableHorror : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "NocturneUnspeakableHorror",
            BuffTextureName = "Nocturne_UnspeakableHorror.dds",
        };
        Particle targetParticle;
        Particle counterParticle;
        Particle particleID1;
        Particle particleID2;
        float baseDamage;
        float fearDuration;
        int timeToFear; // UNUSED
        bool feared;
        float lastTimeExecuted;
        float lastTimeExecuted3;
        float lastTimeExecuted4;
        public NocturneUnspeakableHorror(float baseDamage = default, float fearDuration = default)
        {
            this.baseDamage = baseDamage;
            this.fearDuration = fearDuration;
        }
        public override void OnActivate()
        {
            TeamId teamOfAttacker;
            int nocturneSkinID;
            teamOfAttacker = GetTeamID(attacker);
            nocturneSkinID = GetSkinID(attacker);
            if(nocturneSkinID == 1)
            {
                SpellEffectCreate(out this.targetParticle, out _, "NocturneUnspeakableHorror_tar_frost.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.counterParticle, out _, "NocturneUnspeakableHorror_counter_frost.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else if(nocturneSkinID == 4)
            {
                SpellEffectCreate(out this.targetParticle, out _, "NocturneUnspeakableHorror_tar_ghost.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.counterParticle, out _, "NocturneUnspeakableHorror_counter_ghost.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.targetParticle, out _, "NocturneUnspeakableHorror_tar.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.counterParticle, out _, "NocturneUnspeakableHorror_counter.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            SpellEffectCreate(out this.particleID1, out _, "NocturneUnspeakableHorror_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "L_hand", default, owner, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out this.particleID2, out _, "NocturneUnspeakableHorror_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "R_hand", default, owner, "spine", default, false, false, false, false, false);
            //RequireVar(this.baseDamage);
            //RequireVar(this.fearDuration);
            this.timeToFear = 3;
            this.feared = false;
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Particle asdf; // UNUSED
            teamID = GetTeamID(owner);
            if(!this.feared)
            {
                SpellEffectCreate(out asdf, out _, "NocturneUnspeakableHorror_break.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
            }
            else
            {
                ApplyDamage(attacker, owner, this.baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.25f, 0, false, false, attacker);
            }
            SpellEffectRemove(this.targetParticle);
            SpellEffectRemove(this.counterParticle);
            SpellEffectRemove(this.particleID1);
            SpellEffectRemove(this.particleID2);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID;
            float distance;
            Particle asdf; // UNUSED
            teamID = GetTeamID(owner);
            if(ExecutePeriodically(0.75f, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, this.baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.25f, 0, false, false, attacker);
            }
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted3, true))
            {
                distance = DistanceBetweenObjects("Owner", "Attacker");
                if(distance >= 465)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                if(attacker.IsDead)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
            if(ExecutePeriodically(2, ref this.lastTimeExecuted4, false))
            {
                this.feared = true;
                ApplyFear(attacker, owner, this.fearDuration);
                SpellEffectCreate(out asdf, out _, "NocturneUnspeakableHorror_fear.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.NocturneUnspeakableHorror), attacker, 0);
            }
        }
    }
}
namespace Spells
{
    public class NocturneUnspeakableHorror : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
        };
        float[] effect0 = {1, 1.25f, 1.5f, 1.75f, 2};
        float[] effect1 = {12.5f, 25, 37.5f, 50, 62.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_FearDuration;
            float nextBuffVars_BaseDamage;
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell3", 1, owner, false, false, true);
            BreakSpellShields(target);
            nextBuffVars_FearDuration = this.effect0[level];
            nextBuffVars_BaseDamage = this.effect1[level];
            AddBuff(attacker, target, new Buffs.NocturneUnspeakableHorror(nextBuffVars_BaseDamage, nextBuffVars_FearDuration), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
    }
}