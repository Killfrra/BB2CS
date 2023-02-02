#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Pounce : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            if(!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Vector3 pos;
            Vector3 nextBuffVars_Pos;
            teamID = GetTeamID(owner);
            pos = GetPointByUnitFacingOffset(owner, 375, 0);
            nextBuffVars_Pos = pos;
            AddBuff(attacker, target, new Buffs.Pounce(nextBuffVars_Pos), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellEffectCreate(out _, out _, "nidalee_cougarPounce_cas.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class Pounce : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Interventionspeed_buf.troy", },
        };
        Vector3 pos;
        public Pounce(Vector3 pos = default)
        {
            this.pos = pos;
        }
        public override void OnActivate()
        {
            Vector3 pos;
            //RequireVar(this.pos);
            pos = this.pos;
            Move(target, pos, 900, 15, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            PlayAnimation("Spell2", 0, owner, false, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, false);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            TeamId teamID;
            int level; // UNUSED
            Particle hi; // UNUSED
            teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SpellEffectCreate(out hi, out _, "nidalee_cougarPounce_land.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle bye; // UNUSED
                BreakSpellShields(unit);
                SpellEffectCreate(out bye, out _, "nidalee_cougar_pounce_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                ApplyDamage(attacker, unit, charVars.PounceDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
            }
        }
    }
}