#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LeonaSolarFlare : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {150, 250, 350};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            float nextBuffVars_Distance; // UNUSED
            Particle a; // UNUSED
            Minion other3;
            float nextBuffVars_DamageAmount;
            int nextBuffVars_Level; // UNUSED
            Region nextBuffVars_Bubble;
            teamOfOwner = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(targetPos, ownerPos);
            nextBuffVars_Distance = distance;
            SpellEffectCreate(out a, out _, "Leona_SolarFlare_cas.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "root", default, attacker, default, default, true, default, default, false, false);
            other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            nextBuffVars_DamageAmount = this.effect0[level];
            nextBuffVars_Level = level;
            AddBuff(attacker, other3, new Buffs.LeonaSolarFlare(nextBuffVars_DamageAmount), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            nextBuffVars_Bubble = AddPosPerceptionBubble(teamOfOwner, 800, targetPos, 4, default, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeonaSolarFlareVision(nextBuffVars_Bubble), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LeonaSolarFlare : BBBuffScript
    {
        float damageAmount;
        Particle particle1;
        Particle particle;
        Particle a; // UNUSED
        public LeonaSolarFlare(float damageAmount = default)
        {
            this.damageAmount = damageAmount;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.level);
            //RequireVar(this.damageAmount);
            //RequireVar(this.distance);
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle1, out this.particle, "Leona_SolarFlare_cas_green.troy", "Leona_SolarFlare_cas_red.troy", teamID ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
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
            int level;
            int nextBuffVars_Level; // UNUSED
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle1);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.a, out _, "Leona_SolarFlare_tar.troy", default, TeamId.TEAM_NEUTRAL, 100, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
            level = GetLevel(attacker);
            nextBuffVars_Level = level;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle targetParticle; // UNUSED
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.8f, 1, false, false, attacker);
                SpellEffectCreate(out targetParticle, out _, "Leona_SolarBarrier_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                AddBuff(attacker, unit, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                AddBuff(attacker, unit, new Buffs.LeonaSolarFlareSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyStun(attacker, unit, 1.5f);
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