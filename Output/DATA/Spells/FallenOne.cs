﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FallenOne : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 180f, 150f, 120f, },
            CastingBreaksStealth = true,
            ChannelDuration = 3f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {250, 400, 550};
        public override void SelfExecute()
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.FallenOneTarget(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void ChannelingSuccessStop()
        {
            Particle particle; // UNUSED
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 0, false, false, attacker);
                SpellEffectCreate(out particle, out _, "FallenOne_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false);
            }
        }
        public override void ChannelingCancelStop()
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellBuffRemove(unit, nameof(Buffs.FallenOneTarget), attacker);
            }
        }
    }
}