#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SkarnerFracture : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        Particle partname; // UNUSED
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if(distance > 600)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 600, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            SpellEffectCreate(out this.partname, out _, "Skarner_Fracture_Cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, default, default, false, false);
        }
    }
}
namespace Buffs
{
    public class SkarnerFracture : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "EzrealEssenceFluxBuff",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
        };
    }
}