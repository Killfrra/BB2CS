#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GalioResoluteSmite : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {-0.24f, -0.28f, -0.32f, -0.36f, -0.4f};
        int[] effect1 = {80, 135, 190, 245, 300};
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            float nextBuffVars_MoveSpeedMod;
            TeamId teamID;
            Particle impactVFX; // UNUSED
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out impactVFX, out _, "galio_concussiveBlast_mis_tar.troy", default, TeamId.TEAM_BLUE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, target, default, default, true, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out impactVFX, out _, "galio_concussiveBlast_mis_tar.troy", default, TeamId.TEAM_PURPLE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, target, default, default, true, default, default, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, missileEndPosition, 230, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                Particle targetVFX1; // UNUSED
                Particle targetVFX2; // UNUSED
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
                SpellEffectCreate(out targetVFX1, out _, "galio_concussiveBlast_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "head", unit.Position, unit, default, default, false, default, default, false, false);
                SpellEffectCreate(out targetVFX2, out _, "galio_concussiveBlast_unit_tar_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, unit.Position, unit, default, default, false, default, default, false, false);
                AddBuff(attacker, unit, new Buffs.GalioResoluteSmite(nextBuffVars_MoveSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
            }
        }
    }
}
namespace Buffs
{
    public class GalioResoluteSmite : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Slow.troy", },
            BuffName = "GalioResoluteSmite",
            BuffTextureName = "Galio_ResoluteSmite.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        public GalioResoluteSmite(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}