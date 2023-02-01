#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MonkeyKingDecoy : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {50, 55, 60, 65, 70};
        public override void SelfExecute()
        {
            Vector3 ownerPos;
            TeamId teamID; // UNUSED
            Vector3 ownerFacing;
            float manaCost;
            Pet other1;
            ownerPos = GetUnitPosition(owner);
            teamID = GetTeamID(owner);
            ownerFacing = GetPointByUnitFacingOffset(owner, 100, 0);
            SetGhosted(owner, true);
            manaCost = this.effect0[level];
            Move(owner, ownerFacing, 3000, 0, 0, ForceMovementType.FIRST_WALL_HIT, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            other1 = CloneUnitPet(owner, nameof(Buffs.MonkeyKingDecoyDummy), 0, ownerPos, 0, 0, true);
            IssueOrder(other1, OrderType.Hold, default, other1);
            PlayAnimation("idle1", 0, other1, false, false, false);
            IncPAR(other1, manaCost, PrimaryAbilityResourceType.MANA);
            FaceDirection(other1, ownerFacing);
            AddBuff((ObjAIBase)owner, other1, new Buffs.MonkeyKingDecoyClone(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MonkeyKingDecoyStealth(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}