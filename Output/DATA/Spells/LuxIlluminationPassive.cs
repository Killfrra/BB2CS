#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxIlluminationPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LuxIlluminationPassive",
            BuffTextureName = "LuxIlluminatingFraulein.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.LuxIlluminatingFraulein)) > 0)
            {
                TeamId teamID;
                Particle motaExplosion; // UNUSED
                teamID = GetTeamID(target);
                ApplyDamage(attacker, target, charVars.IlluminateDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                SpellEffectCreate(out motaExplosion, out _, "LuxPassive_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                SpellBuffRemove(target, nameof(Buffs.LuxIlluminatingFraulein), attacker, 0);
            }
        }
        public override void OnUpdateActions()
        {
            SetBuffToolTipVar(1, charVars.IlluminateDamage);
        }
    }
}