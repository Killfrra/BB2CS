#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaZenithBlade : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
        }
    }
}
namespace Spells
{
    public class LeonaZenithBlade : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 700)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 700, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.LeonaZenithBlade(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}