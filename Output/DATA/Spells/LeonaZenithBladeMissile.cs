#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaZenithBladeMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Blind",
            BuffTextureName = "Teemo_TranquilizingShot.dds",
            SpellToggleSlot = 1,
        };
        Vector3 destination;
        float distance;
        public LeonaZenithBladeMissile(Vector3 destination = default, float distance = default)
        {
            this.destination = destination;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.destination);
            //RequireVar(this.distance);
            Move(owner, this.destination, 1900, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, this.distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnMoveEnd()
        {
            Particle temp; // UNUSED
            SpellEffectCreate(out temp, out _, "Leona_ZenithBlade_arrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
        }
        public override void OnMoveSuccess()
        {
            if(attacker is Champion)
            {
                if(attacker.Team != owner.Team)
                {
                    IssueOrder(owner, OrderType.AttackTo, default, attacker);
                }
            }
        }
    }
}
namespace Spells
{
    public class LeonaZenithBladeMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 100, 140, 180, 220};
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            TeamId teamID;
            Vector3 ownerPos; // UNUSED
            float distance;
            float finalDistance;
            Vector3 targetPos;
            Vector3 nextBuffVars_Destination;
            float nextBuffVars_Distance;
            Particle ar1; // UNUSED
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            teamID = GetTeamID(owner);
            ownerPos = GetUnitPosition(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 3000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectDead, 1, nameof(Buffs.LeonaZenithBladeBuffOrder), true))
            {
                FaceDirection(owner, unit.Position);
                distance = DistanceBetweenObjects("Owner", "Unit");
                finalDistance = distance + 225;
                targetPos = GetPointByUnitFacingOffset(owner, finalDistance, 0);
                AddBuff((ObjAIBase)owner, unit, new Buffs.LeonaZenithBladeRoot(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false, false);
                nextBuffVars_Destination = targetPos;
                nextBuffVars_Distance = distance;
                AddBuff((ObjAIBase)unit, owner, new Buffs.LeonaZenithBladeMissile(nextBuffVars_Destination, nextBuffVars_Distance), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out ar1, out _, "Leona_ZenithBlade_trail.troy", default, teamID, 225, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float damageToDeal;
            Particle temp; // UNUSED
            teamID = GetTeamID(attacker);
            if(target is Champion)
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, attacker.Position, 3000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectDead, nameof(Buffs.LeonaZenithBladeBuffOrder), true))
                {
                    SpellBuffClear(unit, nameof(Buffs.LeonaZenithBladeBuffOrder));
                }
                AddBuff(attacker, target, new Buffs.LeonaZenithBladeBuffOrder(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            BreakSpellShields(target);
            AddBuff(attacker, target, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            damageToDeal = this.effect0[level];
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
            if(target is not Champion)
            {
                SpellEffectCreate(out temp, out _, "Leona_ZenithBlade_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            }
        }
    }
}