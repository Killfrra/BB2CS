#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FrostArcherIceArrowAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseAttackDamage;
            DebugSay(owner, "execute");
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    DebugSay(owner, "add buff");
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FrostArcherIceArrowAttack(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true);
                }
            }
            baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false);
        }
    }
}
namespace Buffs
{
    public class FrostArcherIceArrowAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Freeze.troy", },
            BuffName = "FrostArrow",
            BuffTextureName = "3022_Frozen_Heart.dds",
        };
        float[] effect0 = {-0.1f, -0.2f, -0.3f, -0.4f, -0.5f};
        int[] effect1 = {0, 0, 0, 0, 0};
        public override void OnActivate()
        {
            DebugSay(owner, "applicator activatre");
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    int level;
                    float nextBuffVars_MovementSpeedMod;
                    int nextBuffVars_AttackSpeedMod; // UNUSED
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_MovementSpeedMod = this.effect0[level];
                    nextBuffVars_AttackSpeedMod = this.effect1[level];
                    AddBuff((ObjAIBase)owner, target, new Buffs.FrostArrow(nextBuffVars_MovementSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true);
                }
            }
        }
    }
}