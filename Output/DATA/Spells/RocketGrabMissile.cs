#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RocketGrabMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 22f, 20f, 18f, 16f, 14f, },
            TriggersSpellCasts = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {60, 120, 180, 240, 300};
        int[] effect1 = {60, 120, 180, 240, 300};
        int[] effect2 = {60, 120, 180, 240, 300};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool isStealthed;
            float distance;
            float time;
            bool nextBuffVars_WillRemove;
            Particle particleID;
            Particle nextBuffVars_ParticleID;
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                distance = DistanceBetweenObjects("Target", "Attacker");
                time = distance / 1350;
                nextBuffVars_WillRemove = false;
                SpellEffectCreate(out particleID, out _, "FistReturn_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "head", default, owner, "R_hand", default, false, false, false, false, false);
                nextBuffVars_ParticleID = particleID;
                AddBuff((ObjAIBase)target, attacker, new Buffs.RocketGrabMissile(nextBuffVars_ParticleID, nextBuffVars_WillRemove), 1, 1, time, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 0, false, false, attacker);
                ApplyStun(attacker, target, 0.6f);
                DestroyMissile(missileNetworkID);
                AddBuff(attacker, target, new Buffs.RocketGrab2(), 1, 1, 0.6f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
            }
            else
            {
                if(target is Champion)
                {
                    distance = DistanceBetweenObjects("Target", "Attacker");
                    time = distance / 1350;
                    nextBuffVars_WillRemove = false;
                    SpellEffectCreate(out particleID, out _, "FistReturn_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "head", default, owner, "R_hand", default, false, false, false, false, false);
                    nextBuffVars_ParticleID = particleID;
                    AddBuff((ObjAIBase)target, attacker, new Buffs.RocketGrabMissile(nextBuffVars_ParticleID, nextBuffVars_WillRemove), 1, 1, time, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 0, false, false, attacker);
                    ApplyStun(attacker, target, 0.6f);
                    DestroyMissile(missileNetworkID);
                    AddBuff(attacker, target, new Buffs.RocketGrab2(), 1, 1, 0.6f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
                else
                {
                    bool canSee;
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        distance = DistanceBetweenObjects("Target", "Attacker");
                        time = distance / 1350;
                        nextBuffVars_WillRemove = false;
                        SpellEffectCreate(out particleID, out _, "FistReturn_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "head", default, owner, "R_hand", default, false, false, false, false, false);
                        nextBuffVars_ParticleID = particleID;
                        AddBuff((ObjAIBase)target, attacker, new Buffs.RocketGrabMissile(nextBuffVars_ParticleID, nextBuffVars_WillRemove), 1, 1, time, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(target);
                        ApplyDamage(attacker, target, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 0, false, false, attacker);
                        ApplyStun(attacker, target, 0.6f);
                        DestroyMissile(missileNetworkID);
                        AddBuff(attacker, target, new Buffs.RocketGrab2(), 1, 1, 0.6f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class RocketGrabMissile : BBBuffScript
    {
        Particle particleID;
        bool willRemove;
        float lastTimeExecuted;
        public RocketGrabMissile(Particle particleID = default, bool willRemove = default)
        {
            this.particleID = particleID;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(nextBuffVars_ParticleID);
            //RequireVar(nextBuffVars_WillRemove);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleID);
        }
        public override void OnUpdateActions()
        {
            if(!this.willRemove)
            {
                if(ExecutePeriodically(0.1f, ref this.lastTimeExecuted, false))
                {
                    this.willRemove = true;
                }
            }
        }
    }
}