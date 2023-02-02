#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BusterShot : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {600, 800, 1000};
        int[] effect1 = {300, 400, 500};
        public override void AdjustCastInfo()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DrawABead)) > 0)
            {
                float bonusCastRange;
                float newCastRange;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                bonusCastRange = level * 90;
                newCastRange = bonusCastRange + 600;
                SetSpellCastRange(newCastRange);
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_KnockBackDistance;
            nextBuffVars_KnockBackDistance = this.effect0[level];
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, target.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle b; // UNUSED
                SpellEffectCreate(out b, out _, "tristana_bustershot_unit_impact.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                AddBuff(attacker, unit, new Buffs.BusterShot(nextBuffVars_KnockBackDistance), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
            }
            ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1.5f, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class BusterShot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Buster Shot",
            BuffTextureName = "Tristana_BusterShot.dds",
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
            SpellFXOverrideSkins = new[]{ "RocketTristana", },
        };
        float knockBackDistance;
        public BusterShot(float knockBackDistance = default)
        {
            this.knockBackDistance = knockBackDistance;
        }
        public override void OnActivate()
        {
            float distance;
            //RequireVar(this.knockBackDistance);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
            distance = DistanceBetweenObjects("Owner", "Attacker");
            distance += this.knockBackDistance;
            MoveAway(owner, attacker.Position, 1500, 5, 5 + distance, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
        }
    }
}