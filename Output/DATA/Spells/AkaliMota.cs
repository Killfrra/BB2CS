#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliMota : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "AkaliMota",
            BuffTextureName = "AkaliMota.dds",
            NonDispellable = false,
        };
        float motaDamage;
        float energyReturn;
        Particle a;
        Particle b;
        bool doOnce;
        public AkaliMota(float motaDamage = default, float energyReturn = default)
        {
            this.motaDamage = motaDamage;
            this.energyReturn = energyReturn;
        }
        public override void OnActivate()
        {
            //RequireVar(this.motaDamage);
            //RequireVar(this.energyReturn);
            //RequireVar(this.vampPercent);
            SpellEffectCreate(out this.a, out _, "akali_markOftheAssasin_marker_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.b, out _, "akali_markOftheAssasin_marker_tar_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            this.doOnce = true;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.b);
        }
        public override void OnUpdateActions()
        {
            if(!this.doOnce)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            ObjAIBase caster;
            TeamId teamID;
            Particle motaExplosion; // UNUSED
            caster = SetBuffCasterUnit();
            if(caster == attacker)
            {
                if(hitResult != HitResult.HIT_Dodge)
                {
                    if(hitResult != HitResult.HIT_Miss)
                    {
                        if(this.doOnce)
                        {
                            teamID = GetTeamID(attacker);
                            this.doOnce = false;
                            ApplyDamage(attacker, owner, this.motaDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                            IncPAR(attacker, this.energyReturn, PrimaryAbilityResourceType.Energy);
                            SpellEffectCreate(out motaExplosion, out _, "akali_mark_impact_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AkaliTwinAP)) > 0)
                            {
                                AddBuff(attacker, attacker, new Buffs.AkaliShadowSwipeHealingParticle(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class AkaliMota : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {45, 70, 95, 120, 145};
        int[] effect1 = {20, 25, 30, 35, 40};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MotaDamage;
            float nextBuffVars_EnergyReturn;
            float nextBuffVars_VampPercent;
            nextBuffVars_MotaDamage = this.effect0[level];
            nextBuffVars_EnergyReturn = this.effect1[level];
            AddBuff(attacker, target, new Buffs.AkaliMota(nextBuffVars_MotaDamage, nextBuffVars_EnergyReturn), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            ApplyDamage(attacker, target, nextBuffVars_MotaDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
            nextBuffVars_VampPercent = charVars.VampPercent;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliTwinAP)) > 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowSwipeHealingParticle(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}