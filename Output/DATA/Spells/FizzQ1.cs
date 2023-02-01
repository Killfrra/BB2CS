#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzQ1 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
    }
}
namespace Spells
{
    public class FizzQ1 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        Particle particleID; // UNUSED
        int[] effect0 = {75, 125, 175, 225, 275};
        public override void SelfExecute()
        {
            Vector3 castPos;
            Vector3 centerPos;
            Vector3 skip;
            TeamId teamID;
            Vector3 startPos;
            Minion other1;
            Vector3 endPos;
            Minion other2;
            AttackableUnit unit; // UNITIALIZED
            castPos = GetCastSpellTargetPos();
            FaceDirection(owner, castPos);
            centerPos = GetPointByUnitFacingOffset(owner, 225, 0);
            PlayAnimation("Attack1", 0.5f, owner, false, true, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.FizzQ1(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.FizzQ1(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            foreach(AttackableUnit unit in GetUnitsInRectangle(owner, centerPos, 80, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.66f, 0, false, false, attacker);
                AddBuff((ObjAIBase)owner, unit, new Buffs.RenektonBloodSplatterTarget(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.FizzUnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            skip = GetPointByUnitFacingOffset(owner, 250, 0);
            Move(owner, skip, 600, 18, 25, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 300, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            CancelAutoAttack(owner, true);
            teamID = GetTeamID(owner);
            startPos = GetPointByUnitFacingOffset(owner, -75, 0);
            other1 = SpawnMinion("TestCube", "TestCube", "idle.lua", startPos, teamID, false, true, true, true, true, true, 10, true, false);
            endPos = GetPointByUnitFacingOffset(owner, 550, 0);
            other2 = SpawnMinion("TestCube", "TestCube", "idle.lua", endPos, teamID, false, true, true, true, true, true, 10, true, false);
            AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other2, other2, new Buffs.ExpirationTimer(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellEffectCreate(out this.particleID, out _, "kennen_btl_beam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, other1, "head", default, other2, "head", default, true, false, false, false, false);
        }
    }
}