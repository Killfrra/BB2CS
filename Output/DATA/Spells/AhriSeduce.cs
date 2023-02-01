#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AhriSeduce : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "AhriSeduce",
            BuffTextureName = "Ahri_Charm.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float slowPercent;
        Particle particle1;
        Particle particle2;
        Particle particle3;
        public AhriSeduce(float slowPercent = default)
        {
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowPercent);
            SetCanAttack(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out this.particle1, out _, "Ahri_Charm_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "head", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particle2, out _, "Ahri_Charm_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "l_hand", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particle3, out _, "Ahri_Charm_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "r_hand", default, owner, default, default, false, false, false, false, false);
            if(owner is Champion)
            {
                IssueOrder(owner, OrderType.MoveTo, default, attacker);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            StopMove(owner);
            SpellEffectRemove(this.particle1);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle3);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            IncPercentMultiplicativeMovementSpeedMod(owner, this.slowPercent);
            IncFlatAttackRangeMod(owner, -600);
        }
    }
}
namespace Spells
{
    public class AhriSeduce : BBSpellScript
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
            if(distance > 1000)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 950, 0);
            }
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
        }
    }
}