#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Propel : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.75f,
            SpellDamageRatio = 0.75f,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, target, new Buffs.Propel(), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.STUN, 0);
        }
    }
}
namespace Buffs
{
    public class Propel : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Propelled",
            BuffTextureName = "Minotaur_Pulverize.dds",
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        public override void OnActivate()
        {
            Vector3 bouncePos;
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
            Move(target, bouncePos, 100, 25, 0);
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage(attacker, owner, 300, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnUpdateActions()
        {
            bool temp;
            temp = IsMoving(owner);
            if(!temp)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}