#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BrandFissure : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        int[] effect0 = {75, 120, 165, 210, 255};
        float[] effect1 = {93.75f, 150, 206.25f, 262.5f, 318.75f};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            Minion other3;
            float nextBuffVars_FissureDamage;
            float nextBuffVars_AblazeBonusDamage;
            targetPos = GetCastSpellTargetPos();
            teamOfOwner = GetTeamID(owner);
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            nextBuffVars_FissureDamage = this.effect0[level];
            nextBuffVars_AblazeBonusDamage = this.effect1[level];
            AddBuff(attacker, other3, new Buffs.BrandFissure(nextBuffVars_FissureDamage, nextBuffVars_AblazeBonusDamage), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class BrandFissure : BBBuffScript
    {
        float fissureDamage;
        float ablazeBonusDamage;
        Particle groundParticleEffect;
        Particle groundParticleEffect2;
        Particle a;
        public BrandFissure(float fissureDamage = default, float ablazeBonusDamage = default)
        {
            this.fissureDamage = fissureDamage;
            this.ablazeBonusDamage = ablazeBonusDamage;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            int brandSkinID;
            //RequireVar(this.fissureDamage);
            //RequireVar(this.ablazeBonusDamage);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            teamOfOwner = GetTeamID(attacker);
            SpellEffectCreate(out this.groundParticleEffect, out this.groundParticleEffect2, "BrandPOF_tar_green.troy", "BrandPOF_tar_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            brandSkinID = GetSkinID(attacker);
            if(brandSkinID == 3)
            {
                SpellEffectCreate(out this.a, out _, "BrandPOF_Frost_charge.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.a, out _, "BrandPOF_charge.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Vector3 ownerPos;
            int brandSkinID;
            Particle b; // UNUSED
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.groundParticleEffect);
            SpellEffectRemove(this.groundParticleEffect2);
            teamID = GetTeamID(owner);
            ownerPos = GetUnitPosition(owner);
            brandSkinID = GetSkinID(attacker);
            if(brandSkinID == 3)
            {
                SpellEffectCreate(out b, out _, "BrandPOF_Frost_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out b, out _, "BrandPOF_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 260, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle d; // UNUSED
                BreakSpellShields(unit);
                if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.BrandAblaze)) > 0)
                {
                    ApplyDamage(attacker, unit, this.ablazeBonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                }
                else
                {
                    ApplyDamage(attacker, unit, this.fissureDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                }
                AddBuff(attacker, unit, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                if(brandSkinID == 3)
                {
                    SpellEffectCreate(out d, out _, "BrandCritAttack_Frost_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out d, out _, "BrandCritAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}