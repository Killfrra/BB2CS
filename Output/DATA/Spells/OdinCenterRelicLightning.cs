#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinCenterRelicLightning : BBBuffScript
    {
        float bounceCounter;
        Particle particleID; // UNUSED
        public OdinCenterRelicLightning(float bounceCounter = default)
        {
            this.bounceCounter = bounceCounter;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            float nextBuffVars_BounceCounter;
            Particle hi; // UNUSED
            //RequireVar(this.bounceCounter);
            teamID = GetTeamID(attacker);
            if(this.bounceCounter <= 2)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OdinCenterRelicLightning), false))
                {
                    SpellEffectCreate(out this.particleID, out _, "kennen_btl_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "root", default, true, default, default, false);
                    this.bounceCounter++;
                    nextBuffVars_BounceCounter = this.bounceCounter;
                    AddBuff(attacker, unit, new Buffs.OdinCenterRelicLightning(nextBuffVars_BounceCounter), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            ApplyDamage(attacker, owner, 80, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            SpellEffectCreate(out hi, out _, "kennen_btl_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false);
        }
    }
}