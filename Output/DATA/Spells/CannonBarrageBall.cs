#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CannonBarrageBall : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {75, 120, 165};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Minion other3;
            float nextBuffVars_DamageAmount;
            teamOfOwner = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, false, true);
            nextBuffVars_DamageAmount = this.effect0[level];
            AddBuff(attacker, other3, new Buffs.CannonBarrageBall(nextBuffVars_DamageAmount), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class CannonBarrageBall : BBBuffScript
    {
        float damageAmount;
        public CannonBarrageBall(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }
        public override void OnActivate()
        {
            Vector3 ownerPos;
            TeamId teamID;
            Particle boom; // UNUSED
            //RequireVar(this.damageAmount);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
            ownerPos = GetUnitPosition(owner);
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out boom, out _, "pirate_cannonBarrage_point.troy", default, TeamId.TEAM_BLUE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out boom, out _, "pirate_cannonBarrage_point.troy", default, TeamId.TEAM_PURPLE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 ownerPos;
            TeamId teamID;
            Particle boom; // UNUSED
            ownerPos = GetUnitPosition(owner);
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                attacker = GetChampionBySkinName("Gangplank", TeamId.TEAM_BLUE);
                SpellEffectCreate(out boom, out _, "pirate_cannonBarrage_tar.troy", default, TeamId.TEAM_BLUE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                attacker = GetChampionBySkinName("Gangplank", TeamId.TEAM_PURPLE);
                SpellEffectCreate(out boom, out _, "pirate_cannonBarrage_tar.troy", default, TeamId.TEAM_PURPLE, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 265, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.2f, 1, false, false, attacker);
            }
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0.8f, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}