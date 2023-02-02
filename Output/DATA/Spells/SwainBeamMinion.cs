#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SwainBeamMinion : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "",
            BuffTextureName = "",
        };
        Particle bParticle;
        Particle cParticle;
        Particle dParticle;
        Particle a; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetNoRender(owner, false);
            SpellEffectCreate(out this.bParticle, out _, "swain_disintegrationBeam_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "head", default, owner, "Bird_head", default, false);
            SpellEffectCreate(out this.cParticle, out _, "swain_disintegrationBeam_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "head", default, owner, "Bird_head", default, false);
            SpellEffectCreate(out this.dParticle, out _, "swain_disintegrationBeam_beam_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "Bird_head", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectCreate(out this.a, out _, "swain_disintegrationBeam_cas_end.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            SpellEffectRemove(this.cParticle);
            SpellEffectRemove(this.bParticle);
            SpellEffectRemove(this.dParticle);
            SetNoRender(owner, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.SwainBeamExpirationTimer(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnUpdateActions()
        {
            FaceDirection(owner, attacker.Position);
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                float distance;
                distance = DistanceBetweenObjects("Attacker", "Owner");
                if(distance >= 605)
                {
                    if(GetBuffCountFromCaster(attacker, default, nameof(Buffs.SwainBeamDamage)) > 0)
                    {
                        SpellBuffClear(attacker, nameof(Buffs.SwainBeamDamage));
                    }
                    if(GetBuffCountFromCaster(attacker, default, nameof(Buffs.SwainBeamDamageMinion)) > 0)
                    {
                        SpellBuffClear(attacker, nameof(Buffs.SwainBeamDamage));
                    }
                    if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.Slow)) > 0)
                    {
                        SpellBuffRemove(attacker, nameof(Buffs.Slow), (ObjAIBase)owner);
                    }
                    SpellBuffRemoveCurrent(owner);
                }
                if(attacker.IsDead)
                {
                    if(GetBuffCountFromCaster(attacker, default, nameof(Buffs.SwainBeamDamage)) > 0)
                    {
                        SpellBuffClear(attacker, nameof(Buffs.SwainBeamDamage));
                    }
                    if(GetBuffCountFromCaster(attacker, default, nameof(Buffs.SwainBeamDamageMinion)) > 0)
                    {
                        SpellBuffClear(attacker, nameof(Buffs.SwainBeamDamageMinion));
                    }
                    if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.Slow)) > 0)
                    {
                        SpellBuffRemove(attacker, nameof(Buffs.Slow), (ObjAIBase)owner);
                    }
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}