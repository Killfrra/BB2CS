#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VeigarDarkMatter : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {120, 170, 220, 270, 320};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Region bubbleID; // UNUSED
            Minion other3;
            float nextBuffVars_DamageAmount;
            teamOfOwner = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            bubbleID = AddPosPerceptionBubble(teamOfOwner, 300, targetPos, 1, default, false);
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, default, true, (Champion)owner);
            nextBuffVars_DamageAmount = this.effect0[level];
            AddBuff(attacker, other3, new Buffs.VeigarDarkMatter(nextBuffVars_DamageAmount), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class VeigarDarkMatter : BBBuffScript
    {
        float damageAmount;
        public VeigarDarkMatter(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Particle a; // UNUSED
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out a, out _, "permission_dark_matter_cas.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            }
            else
            {
                SpellEffectCreate(out a, out _, "permission_dark_matter_cas.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            }
            //RequireVar(this.damageAmount);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            int veigarSkinID;
            Particle a; // UNUSED
            teamID = GetTeamID(owner);
            veigarSkinID = GetSkinID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                if(veigarSkinID == 4)
                {
                    SpellEffectCreate(out a, out _, "permission_dark_matter_tar_leprechaun.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "permission_dark_matter_tar.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                }
            }
            else
            {
                if(veigarSkinID == 4)
                {
                    SpellEffectCreate(out a, out _, "permission_dark_matter_tar_leprechaun.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "permission_dark_matter_tar.troy", default, TeamId.TEAM_PURPLE, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                }
            }
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 240, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}