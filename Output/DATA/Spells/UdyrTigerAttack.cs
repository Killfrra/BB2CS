#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UdyrTigerAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float baseDamage;
            teamID = GetTeamID(attacker);
            baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    Particle a; // UNUSED
                    Particle agsdg; // UNUSED
                    SpellEffectCreate(out a, out _, "udyr_tiger_claw_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                    SpellEffectCreate(out agsdg, out _, "udyr_tiger_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class UdyrTigerAttack : BBBuffScript
    {
    }
}