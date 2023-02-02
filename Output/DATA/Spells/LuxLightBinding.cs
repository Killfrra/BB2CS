﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LuxLightBinding : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
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
            if(distance > 1300)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1150, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class LuxLightBinding : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "LuxLightBinding_tar.troy", "", },
            BuffName = "LuxLightBindingMis",
            BuffTextureName = "LuxCrashingBlitzReady.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
    }
}