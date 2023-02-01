#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TaricHammerSmash : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Particle partname; // UNUSED
        int[] effect0 = {30, 50, 70};
        int[] effect1 = {30, 50, 70};
        int[] effect2 = {150, 250, 350};
        public override void SelfExecute()
        {
            TeamId teamID;
            Particle hi1; // UNUSED
            float nextBuffVars_DamageIncrease;
            float nextBuffVars_AbilityPower;
            Particle shatterz; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out hi1, out _, "TaricHammerSmash_shatter.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.partname, out _, "TaricHammerSmash_nova.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            nextBuffVars_DamageIncrease = this.effect0[level];
            nextBuffVars_AbilityPower = this.effect1[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                SpellEffectCreate(out shatterz, out _, "Taric_GemStorm_Tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
            }
            AddBuff(attacker, attacker, new Buffs.Radiance(nextBuffVars_DamageIncrease, nextBuffVars_AbilityPower), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}