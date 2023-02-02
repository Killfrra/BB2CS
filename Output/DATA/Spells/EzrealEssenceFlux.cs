#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class EzrealEssenceFlux : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
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
            if(distance > 1050)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1000, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class EzrealEssenceFlux : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "EzrealEssenceFluxBuff",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
            SpellFXOverrideSkins = new[]{ "CyberEzreal", },
        };
        float attackSpeedMod;
        public EzrealEssenceFlux(float attackSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}