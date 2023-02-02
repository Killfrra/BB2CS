#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CassiopeiaPetrifyingGazeSpell : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = {-0.6f, -0.6f, -0.6f};
        int[] effect1 = {200, 325, 450};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            BreakSpellShields(target);
            teamID = GetTeamID(target);
            if(IsInFront(target, attacker))
            {
                AddBuff(attacker, target, new Buffs.CassiopeiaPetrifyingGaze(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
            }
            else
            {
                float nextBuffVars_MoveSpeedMod;
                Particle particle2; // UNUSED
                nextBuffVars_MoveSpeedMod = this.effect0[level];
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
                SpellEffectCreate(out particle2, out _, "CassPetrifyMiss_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, "root", default, target, default, default, true, default, default, false);
            }
            ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
        }
    }
}