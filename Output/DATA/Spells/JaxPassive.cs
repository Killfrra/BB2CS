#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JaxPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "JaxPassive",
            BuffTextureName = "Armsmaster_CoupDeGrace.dds",
            PersistsThroughDeath = true,
        };
        float[] effect0 = {0.04f, 0.04f, 0.04f, 0.06f, 0.06f, 0.06f, 0.08f, 0.08f, 0.08f, 0.1f, 0.1f, 0.1f, 0.12f, 0.12f, 0.12f, 0.14f, 0.14f, 0.14f};
        int[] effect1 = {4, 4, 4, 6, 6, 6, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14, 14, 14};
        public override void OnUpdateStats()
        {
            int count;
            int level;
            float aS;
            count = GetBuffCountFromAll(owner, nameof(Buffs.JaxRelentlessAssaultAS));
            if(count > 0)
            {
                level = GetLevel(owner);
                aS = this.effect0[level];
                aS *= count;
                IncPercentAttackSpeedMod(owner, aS);
            }
        }
        public override void OnUpdateActions()
        {
            int level;
            float aS;
            level = GetLevel(owner);
            aS = this.effect1[level];
            SetBuffToolTipVar(1, aS);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int ult;
            int count;
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    ult = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultAS(), charVars.UltStacks, 1, 2.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    if(ult > 0)
                    {
                        count = GetBuffCountFromAll(owner, nameof(Buffs.JaxRelentlessAssaultDebuff));
                        if(count == 0)
                        {
                            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.JaxRelentlessAttack)) == 0)
                            {
                                AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultDebuff(), 3, 2, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            }
                            else
                            {
                                AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultDebuff(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            }
                        }
                        if(count < 3)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultDebuff(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        }
                        else
                        {
                            SpellBuffClear(owner, nameof(Buffs.JaxRelentlessAssaultDebuff));
                            AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAttack(), 1, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}