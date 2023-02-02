#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearRChain : BBBuffScript
    {
        float bounceCounter;
        float volibearRDamage;
        float volibearRRatio;
        Particle particleID; // UNUSED
        public VolibearRChain(float bounceCounter = default, float volibearRDamage = default, float volibearRRatio = default)
        {
            this.bounceCounter = bounceCounter;
            this.volibearRDamage = volibearRDamage;
            this.volibearRRatio = volibearRRatio;
        }
        public override void OnActivate()
        {
            bool last;
            TeamId teamID;
            Particle c; // UNUSED
            float championPriority;
            float nextBuffVars_BounceCounter;
            TeamId unitTeamID;
            TeamId ownerTeamID;
            float nextBuffVars_VolibearRDamage;
            float nextBuffVars_VolibearRRatio;
            last = true;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out c, out _, "Volibear_R_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CENTER_LOC", default, owner, default, default, true, false, false, false, false);
            //RequireVar(this.bounceCounter);
            //RequireVar(this.volibearRRatio);
            //RequireVar(this.volibearRDamage);
            championPriority = 0;
            this.volibearRDamage *= 1;
            this.volibearRRatio *= 1;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                championPriority++;
            }
            if(championPriority > 0)
            {
                if(this.bounceCounter <= 3)
                {
                    foreach(AttackableUnit unit in GetRandomVisibleUnitsInArea(attacker, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.VolibearRChain), false))
                    {
                        SpellEffectCreate(out this.particleID, out _, "volibear_R_chain_lighting_01.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "root", default, true, false, false, false, false);
                        this.bounceCounter++;
                        nextBuffVars_BounceCounter = this.bounceCounter;
                        nextBuffVars_VolibearRDamage = this.volibearRDamage;
                        nextBuffVars_VolibearRRatio = this.volibearRRatio;
                        championPriority += 999;
                        unitTeamID = GetTeamID(unit);
                        AddUnitPerceptionBubble(unitTeamID, 10, owner, 0.75f, default, owner, false);
                        AddBuff(attacker, unit, new Buffs.VolibearRChain(nextBuffVars_BounceCounter, nextBuffVars_VolibearRDamage, nextBuffVars_VolibearRRatio), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        last = false;
                    }
                }
            }
            if(championPriority < 4)
            {
                if(this.bounceCounter <= 3)
                {
                    foreach(AttackableUnit unit in GetRandomVisibleUnitsInArea(attacker, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, nameof(Buffs.VolibearRChain), false))
                    {
                        SpellEffectCreate(out this.particleID, out _, "volibear_R_chain_lighting_01.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "root", default, true, false, false, false, false);
                        this.bounceCounter++;
                        nextBuffVars_BounceCounter = this.bounceCounter;
                        nextBuffVars_VolibearRDamage = this.volibearRDamage;
                        nextBuffVars_VolibearRRatio = this.volibearRRatio;
                        unitTeamID = GetTeamID(unit);
                        AddUnitPerceptionBubble(unitTeamID, 10, owner, 0.75f, default, owner, false);
                        AddBuff(attacker, unit, new Buffs.VolibearRChain(nextBuffVars_BounceCounter, nextBuffVars_VolibearRDamage, nextBuffVars_VolibearRRatio), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        last = false;
                    }
                }
            }
            if(last)
            {
                Particle targetParticle; // UNUSED
                teamID = GetTeamID(owner);
                SpellEffectCreate(out targetParticle, out _, "Volibear_R_lasthit_sound.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            ownerTeamID = GetTeamID(owner);
            AddUnitPerceptionBubble(ownerTeamID, 250, attacker, 0.75f, default, default, false);
            BreakSpellShields(owner);
            ApplyDamage(attacker, owner, this.volibearRDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, this.volibearRRatio, 0, false, false, attacker);
        }
    }
}