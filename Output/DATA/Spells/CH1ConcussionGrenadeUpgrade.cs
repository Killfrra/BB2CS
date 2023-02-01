#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class CH1ConcussionGrenadeUpgrade : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 135, 190, 245, 300};
        float[] effect1 = {1, 1.5f, 2, 2.5f, 3};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Particle arr; // UNUSED
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out arr, out _, "heimerdinger_CH1_grenade_tar.troy", default, TeamId.TEAM_BLUE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, target, default, default, true);
            }
            else
            {
                SpellEffectCreate(out arr, out _, "heimerdinger_CH1_grenade_tar.troy", default, TeamId.TEAM_PURPLE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, target.Position, target, default, default, true);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, default, true))
            {
                BreakSpellShields(unit);
                if(unit is BaseTurret)
                {
                }
                else
                {
                    ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                }
                SpellEffectCreate(out arr, out _, "heimerdinger_CH1_grenade_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, unit.Position, unit, default, default, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.BlindingDart(), 100, 1, this.effect1[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.BLIND, 0, true, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 125, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyStun(attacker, unit, 1.5f);
            }
        }
    }
}