#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SweepingBlow : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.SweepingBlow(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false);
            ApplyDamage(attacker, target, 200, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class SweepingBlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
            MoveAway(owner, attacker.Position, 1200, 20, 600, 550, ForceMovementType.FIRST_COLLISION_HIT, ForceMovementOrdersType.CANCEL_ORDER, 0);
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