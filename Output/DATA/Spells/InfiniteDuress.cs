#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class InfiniteDuress : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "InfiniteDuress",
            BuffTextureName = "Wolfman_InfiniteDuress.dds",
        };
        float lifestealBonus;
        public InfiniteDuress(float lifestealBonus = default)
        {
            this.lifestealBonus = lifestealBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifestealBonus);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, this.lifestealBonus);
        }
    }
}
namespace Spells
{
    public class InfiniteDuress : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {200, 300, 400};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_LifestealBonus;
            float nextBuffVars_hitsRemaining;
            float nextBuffVars_damagePerTick;
            Vector3 pos;
            bool canMove;
            float baseDamage;
            float totalAD;
            float bonusDamage;
            float totalDamage;
            float damagePerTick;
            AddBuff((ObjAIBase)owner, target, new Buffs.Suppression(), 100, 1, 1.8f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SUPPRESSION, 0, true, false, false);
            pos = GetRandomPointInAreaUnit(target, 150, 150);
            TeleportToPosition(owner, pos);
            FaceDirection(attacker, target.Position);
            canMove = GetCanMove(target);
            if(!canMove)
            {
                nextBuffVars_LifestealBonus = 0.3f;
                nextBuffVars_hitsRemaining = 5;
                AddBuff(attacker, attacker, new Buffs.InfiniteDuress(nextBuffVars_LifestealBonus), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                baseDamage = this.effect0[level];
                totalAD = GetTotalAttackDamage(owner);
                bonusDamage = 1.667f * totalAD;
                totalDamage = baseDamage + bonusDamage;
                damagePerTick = totalDamage / nextBuffVars_hitsRemaining;
                nextBuffVars_damagePerTick = damagePerTick;
                AddBuff((ObjAIBase)owner, target, new Buffs.InfiniteDuressChannel(nextBuffVars_hitsRemaining, nextBuffVars_damagePerTick), 1, 1, 1.8f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.InfiniteDuressSound(), 1, 1, 1.8f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellCast(attacker, target, target.Position, target.Position, 0, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
            }
            else
            {
                IssueOrder(owner, OrderType.AttackTo, default, target);
            }
        }
    }
}