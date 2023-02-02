#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OrianaDissonanceBackup : BBSpellScript
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
            float castRange;
            float distance;
            bool nextBuffVars_GhostAlive; // UNUSED
            bool deployed;
            Vector3 castPos;
            Vector3 nextBuffVars_CastPos; // UNUSED
            Vector3 nextBuffVars_TargetPos; // UNUSED
            targetPos = GetCastSpellTargetPos();
            FaceDirection(owner, targetPos);
            ownerPos = GetUnitPosition(owner);
            castRange = 1640;
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            if(distance > castRange)
            {
                targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
            }
            nextBuffVars_GhostAlive = charVars.GhostAlive;
            deployed = false;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetCastSpellTargetPos();
                distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                if(distance > castRange)
                {
                    targetPos = GetPointByUnitFacingOffset(owner, castRange, 0);
                }
                castPos = GetUnitPosition(unit);
                nextBuffVars_CastPos = castPos;
                nextBuffVars_TargetPos = targetPos;
                SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 5, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
            }
            if(!deployed)
            {
                if(charVars.GhostAlive)
                {
                }
                else
                {
                    castPos = GetUnitPosition(owner);
                    nextBuffVars_CastPos = castPos;
                    nextBuffVars_TargetPos = targetPos;
                    SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 5, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                }
            }
            PlayAnimation("Spell2", 1.25f, owner, true, false, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}