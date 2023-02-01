#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzMarinerDoom : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "",
            BuffTextureName = "",
        };
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            object missileId; // UNITIALIZED
            charVars.UltMissileID = missileId;
            SpellBuffClear(owner, nameof(Buffs.FizzMarinerDoom));
        }
    }
}
namespace Spells
{
    public class FizzMarinerDoom : BBSpellScript
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
            if(distance > 1250)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1250, 0);
            }
            else if(distance <= 200)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 200, 0);
            }
            else
            {
                distance += 50;
                targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.FizzMarinerDoom(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            charVars.UltFired = true;
        }
    }
}