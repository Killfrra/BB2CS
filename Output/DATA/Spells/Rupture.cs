#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Rupture : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 135, 190, 245, 305};
        float[] effect1 = {-0.6f, -0.6f, -0.6f, -0.6f, -0.6f};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            Minion other3;
            float nextBuffVars_DamageAmount;
            float nextBuffVars_MoveSpeedMod;
            targetPos = GetCastSpellTargetPos();
            teamOfOwner = GetTeamID(owner);
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, false, true, (Champion)owner);
            nextBuffVars_DamageAmount = this.effect0[level];
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            AddBuff(attacker, other3, new Buffs.Rupture(nextBuffVars_DamageAmount, nextBuffVars_MoveSpeedMod), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Rupture : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            SpellVOOverrideSkins = new[]{ "DandyChogath", },
        };
        float damageAmount;
        float moveSpeedMod;
        public Rupture(float damageAmount = default, float moveSpeedMod = default)
        {
            this.damageAmount = damageAmount;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Particle a; // UNUSED
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out a, out _, "rupture_cas_01.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "rupture_cas_01.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            }
            //RequireVar(this.damageAmount);
            //RequireVar(this.moveSpeedMod);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            int skin;
            Particle a; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            teamID = GetTeamID(owner);
            skin = GetSkinID(owner);
            if(skin == 4)
            {
                if(teamID == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out a, out _, "rupture_dino_cas_02.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "rupture_dino_cas_02.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
            }
            else
            {
                if(teamID == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out a, out _, "rupture_cas_02.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "rupture_cas_02.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                }
            }
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.RuptureLaunch(nextBuffVars_MoveSpeedMod), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, true, false);
                ApplyDamage(attacker, unit, this.damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
    }
}