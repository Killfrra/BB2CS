#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FallenOneExtra : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 3f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {250, 400, 550};
        public override void ChannelingSuccessStop()
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes))
            {
                Particle particle; // UNUSED
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, default, false, false);
                SpellEffectCreate(out particle, out _, "FallenOne_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false);
            }
        }
    }
}