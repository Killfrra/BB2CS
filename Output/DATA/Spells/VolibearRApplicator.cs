#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearRApplicator : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "L_BUFFBONE_GLB_HAND_LOC", "R_BUFFBONE_GLB_HAND_LOC", null, "C_BUFFBONE_GLB_CENTER_LOC", },
            AutoBuffActivateEffect = new[]{ "volibear_R_attack_buf_left.troy", "volibear_R_attack_buf_right.troy", "Volibear_R_cas.troy", "Volibear_R_cas_02.troy", },
            BuffName = "VolibearRApplicator",
            BuffTextureName = "VolibearR.dds",
        };
        int volibearRDamage;
        float volibearRSpeed;
        float volibearRRatio;
        Particle b;
        Particle c;
        Particle a;
        Particle d;
        Particle particleID; // UNUSED
        public VolibearRApplicator(int volibearRDamage = default, float volibearRSpeed = default, float volibearRRatio = default)
        {
            this.volibearRDamage = volibearRDamage;
            this.volibearRSpeed = volibearRSpeed;
            this.volibearRRatio = volibearRRatio;
        }
        public override void OnActivate()
        {
            //RequireVar(this.volibearRRatio);
            //RequireVar(this.volibearRDamage);
            //RequireVar(this.volibearRSpeed);
            SpellEffectCreate(out this.b, out _, "volibear_R_cas_03.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.c, out _, "Volibear_R_cas_04.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.a, out _, "volibear_R_lightning_arms.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_middle_finger", default, target, "r_uparm", default, false, false, false, false, false);
            SpellEffectCreate(out this.d, out _, "volibear_R_lightning_arms.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_middle_finger", default, target, "l_uparm", default, false, false, false, false, false);
            IncPercentAttackSpeedMod(owner, this.volibearRSpeed);
            IncScaleSkinCoef(0.08f, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.b);
            SpellEffectRemove(this.c);
            SpellEffectRemove(this.d);
        }
        public override void OnUpdateStats()
        {
            //RequireVar(this.volibearRSpeed);
            IncPercentAttackSpeedMod(owner, this.volibearRSpeed);
            IncScaleSkinCoef(0.08f, owner);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId teamID;
            ObjAIBase caster; // UNUSED
            Particle kennenss; // UNUSED
            Particle c; // UNUSED
            int nextBuffVars_BounceCounter;
            float nextBuffVars_VolibearRDamage;
            float nextBuffVars_VolibearRRatio;
            teamID = GetTeamID(owner);
            if(target is not BaseTurret)
            {
                if(target is ObjAIBase)
                {
                    if(hitResult != HitResult.HIT_Dodge)
                    {
                        if(hitResult != HitResult.HIT_Miss)
                        {
                            caster = SetBuffCasterUnit();
                            if(attacker is not Champion)
                            {
                                caster = GetPetOwner((Pet)attacker);
                            }
                            SpellEffectCreate(out this.particleID, out _, "volibear_R_chain_lighting_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, attacker, "head", default, target, "root", default, true, false, false, false, false);
                            SpellEffectCreate(out kennenss, out _, "Volibear_R_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, target, "C_BUFFBONE_GLB_CENTER_LOC", default, target, default, default, true, false, false, false, false);
                            SpellEffectCreate(out kennenss, out _, "Volibear_R_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, target, default, default, target, default, default, true, false, false, false, false);
                            SpellEffectCreate(out c, out _, "Volibear_R_cas_04.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                            nextBuffVars_BounceCounter = 1;
                            nextBuffVars_VolibearRDamage = this.volibearRDamage;
                            nextBuffVars_VolibearRRatio = this.volibearRRatio;
                            AddBuff(attacker, target, new Buffs.VolibearRChain(nextBuffVars_BounceCounter, nextBuffVars_VolibearRDamage, nextBuffVars_VolibearRRatio), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}