#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JaxRelentlessAssault : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "JaxRelentlessAssault",
            BuffTextureName = "Armsmaster_CoupDeGrace.dds",
            PersistsThroughDeath = true,
        };
        float[] effect0 = {0.03f, 0.03f, 0.03f, 0.04f, 0.04f, 0.04f, 0.05f, 0.05f, 0.05f, 0.06f, 0.06f, 0.06f, 0.07f, 0.07f, 0.07f, 0.08f, 0.08f, 0.08f};
        float[] effect1 = {0.03f, 0.03f, 0.03f, 0.04f, 0.04f, 0.04f, 0.05f, 0.05f, 0.05f, 0.06f, 0.06f, 0.06f, 0.07f, 0.07f, 0.07f, 0.08f, 0.08f, 0.08f};
        int[] effect2 = {20, 20, 20, 30, 30, 30, 40, 40, 40, 50, 50, 50, 60, 60, 60, 70, 70, 70};
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
            float dmg;
            float aP;
            level = GetLevel(owner);
            aS = this.effect1[level];
            SetBuffToolTipVar(1, aS);
            dmg = this.effect2[level];
            SetBuffToolTipVar(2, dmg);
            aP = GetFlatMagicDamageMod(owner);
            SetBuffToolTipVar(3, aP);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int ult;
            int count;
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    DebugSay(owner, "2");
                    AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultAS(), charVars.UltStacks, 1, 2.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    ult = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(ult > 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.JaxRelentlessAssaultAS)) == 0)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultDebuff(), 2, 2, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            DebugSay(owner, "1");
                        }
                        count = GetBuffCountFromAll(owner, nameof(Buffs.JaxRelentlessAssaultDebuff));
                        if(count < 2)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAssaultDebuff(), 2, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            DebugSay(owner, "2");
                        }
                        else
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.JaxRelentlessAttack(), 1, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                            SpellBuffClear(owner, nameof(Buffs.JaxRelentlessAssaultDebuff));
                            DebugSay(owner, "3");
                        }
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class JaxRelentlessAssault : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.3f, 0.35f, 0.4f};
        int[] effect1 = {6, 6, 6};
        public override void SelfExecute()
        {
            float nextBuffVars_SpeedBoost;
            float duration;
            nextBuffVars_SpeedBoost = this.effect0[level];
            duration = this.effect1[level];
            AddBuff(attacker, owner, new Buffs.JaxRelentlessAssaultSpeed(), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}