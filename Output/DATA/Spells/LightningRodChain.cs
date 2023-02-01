#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LightningRodChain : BBBuffScript
    {
        float bounceCounter;
        Particle particleID; // UNUSED
        public LightningRodChain(float bounceCounter = default)
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
            if(this.bounceCounter <= 3)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.LightningRodChain), false))
                {
                    SpellEffectCreate(out this.particleID, out _, "kennen_btl_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "root", default, true, false, false, false, false);
                    this.bounceCounter++;
                    nextBuffVars_BounceCounter = this.bounceCounter;
                    AddBuff(attacker, unit, new Buffs.LightningRodChain(nextBuffVars_BounceCounter), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            ApplyDamage(attacker, owner, 110, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            SpellEffectCreate(out hi, out _, "kennen_btl_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
    }
}