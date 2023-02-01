#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3124 : BBItemScript
    {
        float lastTimeExecuted;
        float cooldownResevoir;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(2.85f, ref this.lastTimeExecuted, false))
            {
                if(this.cooldownResevoir < 2)
                {
                    this.cooldownResevoir++;
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(!spellVars.DoesntTriggerSpellCasts)
            {
                if(this.cooldownResevoir > 0)
                {
                    if(this.cooldownResevoir == 2)
                    {
                        this.lastTimeExecuted = GetTime();
                    }
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Rageblade(), 8, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0);
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Rageblade(), 8, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0);
        }
        public override void OnActivate()
        {
            this.cooldownResevoir = 2;
        }
        public override void OnDeactivate()
        {
            IncPermanentPercentAttackSpeedMod(owner, -0.04f);
            IncPermanentFlatMagicDamageMod(owner, -7);
        }
        public override void OnBeingDodged()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Rageblade(), 8, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0);
        }
        public override void OnMiss()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.Rageblade(), 8, 1, 5, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class _3124 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rageblade",
            BuffTextureName = "139_Strygwyrs_Reaver.dds",
        };
        public override void OnActivate()
        {
            IncPermanentPercentAttackSpeedMod(owner, 0.04f);
            IncPermanentFlatMagicDamageMod(owner, 7);
        }
    }
}