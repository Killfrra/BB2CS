#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OrianaRedactCommand : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 120, 160, 200, 240};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamageBlock;
            bool deployed;
            float baseDamageBlock;
            float selfAP;
            float bonusShield;
            float totalShield;
            Vector3 castPos;
            Vector3 targetPos;
            float minDistance;
            SetSpellOffsetTarget(1, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            SetSpellOffsetTarget(3, SpellSlotType.SpellSlots, nameof(Spells.JunkName), SpellbookType.SPELLBOOK_CHAMPION, owner, owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellBuffClear(unit, nameof(Buffs.OrianaRedactTarget));
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, target, new Buffs.OrianaRedactTarget(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            deployed = false;
            baseDamageBlock = this.effect0[level];
            selfAP = GetFlatMagicDamageMod(owner);
            bonusShield = selfAP * 0.4f;
            totalShield = bonusShield + baseDamageBlock;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, 1, nameof(Buffs.OrianaGhost), true))
            {
                SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
                SpellBuffClear(owner, nameof(Buffs.OrianaBlendDelay));
                deployed = true;
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.OrianaGhost)) > 0)
                {
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff((ObjAIBase)owner, target, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    SpellBuffClear(unit, nameof(Buffs.OrianaGhost));
                    SpellBuffClear(owner, nameof(Buffs.OrianaBlendDelay));
                    castPos = GetUnitPosition(unit);
                    targetPos = GetUnitPosition(target);
                    minDistance = DistanceBetweenPoints(castPos, targetPos);
                    if(minDistance <= 100)
                    {
                        nextBuffVars_DamageBlock = totalShield;
                        AddBuff((ObjAIBase)owner, target, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        if(target != owner)
                        {
                            AddBuff((ObjAIBase)owner, target, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff((ObjAIBase)owner, target, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                    else
                    {
                        nextBuffVars_DamageBlock = totalShield;
                        AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaRedact(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                }
            }
            if(!deployed)
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.OriannaBallTracker)) > 0)
                {
                    SpellBuffClear(owner, nameof(Buffs.OriannaBallTracker));
                    targetPos = GetUnitPosition(target);
                    castPos = charVars.BallPosition;
                    minDistance = DistanceBetweenPoints(castPos, targetPos);
                    if(minDistance <= 100)
                    {
                        nextBuffVars_DamageBlock = totalShield;
                        AddBuff((ObjAIBase)owner, target, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        if(target != owner)
                        {
                            AddBuff((ObjAIBase)owner, target, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff((ObjAIBase)owner, target, new Buffs.OrianaGhostSelf(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                    else
                    {
                        nextBuffVars_DamageBlock = totalShield;
                        AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaRedact(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                }
                else if(target != owner)
                {
                    SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
                    SpellBuffClear(owner, nameof(Buffs.OrianaBlendDelay));
                    castPos = GetUnitPosition(owner);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaRedact(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                }
                else
                {
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            PlayAnimation("Spell3", 0, owner, false, true, false);
        }
    }
}