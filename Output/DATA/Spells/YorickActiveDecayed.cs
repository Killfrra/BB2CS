#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class YorickActiveDecayed : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
        int[] effect0 = {80, 140, 200, 260, 320};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Particle e; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            SpellEffectCreate(out e, out _, "YorickPHDecayedExplosion.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false, false);
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = -0.5f;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle b; // UNUSED
                BreakSpellShields(unit);
                SpellEffectCreate(out b, out _, "tristana_explosiveShot_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.YorickDecayedSlow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}