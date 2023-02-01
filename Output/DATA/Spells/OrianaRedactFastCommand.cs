#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OrianaRedactFastCommand : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {40, 70, 100, 130, 160};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamageBlock;
            bool deployed;
            float baseDamageBlock;
            float selfAP;
            float bonusShield;
            float totalShield;
            Vector3 castPos;
            PlayAnimation("Spell2", 1.25f, owner, false, false, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaGlobalCooldown(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, target, new Buffs.OrianaRedactTarget(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            deployed = false;
            baseDamageBlock = this.effect0[level];
            selfAP = GetFlatMagicDamageMod(owner);
            bonusShield = selfAP * 0.7f;
            totalShield = bonusShield + baseDamageBlock;
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.OrianaGhost), true))
            {
                SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
                deployed = true;
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.OrianaGhost)) > 0)
                {
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff((ObjAIBase)owner, target, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    SpellBuffClear(unit, nameof(Buffs.OrianaGhost));
                    castPos = GetUnitPosition(unit);
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaRedact(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OrianaDesperatePower)) > 0)
                    {
                        SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                    else
                    {
                        SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            if(!deployed)
            {
                if(charVars.GhostAlive)
                {
                }
                else if(target != owner)
                {
                    SpellBuffClear(owner, nameof(Buffs.OrianaGhostSelf));
                    castPos = GetUnitPosition(owner);
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaRedact(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OrianaDesperatePower)) > 0)
                    {
                        SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                    else
                    {
                        SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 2, SpellSlotType.ExtraSlots, level, false, true, false, false, false, true, castPos);
                    }
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    nextBuffVars_DamageBlock = totalShield;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaRedactShield(nextBuffVars_DamageBlock), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}