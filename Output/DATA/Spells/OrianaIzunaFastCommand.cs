#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OrianaIzunaFastCommand : BBSpellScript
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
            bool nextBuffVars_GhostAlive;
            Vector3 nextBuffVars_CastPos;
            Vector3 nextBuffVars_TargetPos;
            bool deployed;
            Vector3 castPos;
            SpellBuffClear(owner, nameof(Buffs._0));
            SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
            targetPos = GetCastSpellTargetPos();
            FaceDirection(owner, targetPos);
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            if(distance > 775)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 800, 0);
            }
            nextBuffVars_GhostAlive = charVars.GhostAlive;
            deployed = false;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OrianaGhost), true))
            {
                deployed = true;
                targetPos = GetCastSpellTargetPos();
                distance = DistanceBetweenObjectAndPoint(owner, targetPos);
                if(distance > 775)
                {
                    targetPos = GetPointByUnitFacingOffset(owner, 750, 0);
                }
                castPos = GetUnitPosition(unit);
                nextBuffVars_CastPos = castPos;
                nextBuffVars_TargetPos = targetPos;
                AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffClear(unit, nameof(Buffs.OrianaGhost));
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OrianaDesperatePower)) > 0)
                {
                    SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                }
                else
                {
                    SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                }
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
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaIzuna(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OrianaDesperatePower)) > 0)
                    {
                        SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                    }
                    else
                    {
                        SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, castPos);
                    }
                }
            }
            PlayAnimation("Spell2", 1.25f, owner, true, false, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}