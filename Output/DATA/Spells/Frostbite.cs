#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Frostbite : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {55, 85, 115, 145, 175};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float baseDamage;
            int count;
            Particle smokeBomb; // UNUSED
            teamID = GetTeamID(owner);
            baseDamage = this.effect0[level];
            count = GetBuffCountFromAll(target, nameof(Buffs.Chilled));
            if(count > 0)
            {
                baseDamage *= 2;
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false);
                SpellEffectCreate(out smokeBomb, out _, "cryo_FrostBite_chilled_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            }
            else
            {
                ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 1, false, false);
            }
        }
    }
}