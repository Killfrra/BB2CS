#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzTrickSlam : BBBuffScript
    {
        int[] effect0 = {70, 120, 170, 220, 270};
        float[] effect1 = {-0.4f, -0.45f, -0.5f, -0.55f, -0.6f};
        public override void OnActivate()
        {
            TeamId teamID;
            Particle temp; // UNUSED
            PlayAnimation("Spell3c", 0, owner, false, false, false);
            IncPercentMovementSpeedMod(owner, 0.5f);
            SetCanMove(owner, true);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out temp, out _, "fizz_playfultrickster_flip_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", owner.Position, owner, default, default, true, true, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            TeamId teamID;
            Particle asdf; // UNUSED
            Particle b; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out asdf, out _, "Fizz_TrickSlam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                SpellEffectCreate(out b, out _, "Fizz_TrickSlam_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                nextBuffVars_MoveSpeedMod = this.effect1[level];
                AddBuff((ObjAIBase)owner, unit, new Buffs.FizzWSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.5f);
        }
    }
}