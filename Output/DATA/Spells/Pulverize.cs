﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Pulverize : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.75f,
            SpellDamageRatio = 0.75f,
        };
        int[] effect0 = {60, 105, 150, 195, 240};
        public override void SelfExecute()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.AlistarTrample(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff((ObjAIBase)owner, unit, new Buffs.Pulverize(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0, true, false, false);
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class Pulverize : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Pulverize",
            BuffTextureName = "Minotaur_Pulverize.dds",
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        public override void OnActivate()
        {
            Vector3 bouncePos;
            bouncePos = GetRandomPointInAreaUnit(owner, 10, 10);
            Move(owner, bouncePos, 10, 20, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 10, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            ApplyStun(attacker, target, 0.5f);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}