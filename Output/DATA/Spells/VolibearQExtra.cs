#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearQExtra : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "GarenSlash",
            BuffTextureName = "17.dds",
            PopupMessage = new[]{ "game_floatingtext_Knockup", },
        };
        Vector3 bouncePos;
        public VolibearQExtra(Vector3 bouncePos = default)
        {
            this.bouncePos = bouncePos;
        }
        public override void OnActivate()
        {
            ObjAIBase caster; // UNUSED
            float idealDistance;
            float speed;
            float gravity;
            //RequireVar(this.bouncePos);
            caster = SetBuffCasterUnit();
            idealDistance = 70;
            speed = 150;
            gravity = 60;
            Move(owner, this.bouncePos, speed, gravity, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, idealDistance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
    }
}
namespace Spells
{
    public class VolibearQExtra : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}