#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "FizzPassive",
            BuffTextureName = "FizzPassive.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float defense;
        int[] effect0 = {4, 4, 4, 6, 6, 6, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14, 14, 14, 16};
        public override void OnActivate()
        {
            this.defense = 4;
            SetBuffToolTipVar(1, this.defense);
            SetGhosted(owner, true);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                if(target is ObjAIBase)
                {
                    if(target is not BaseTurret)
                    {
                        AddBuff((ObjAIBase)owner, target, new Buffs.FizzSeastoneTrident(), 1, 1, 3.1f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
            {
                damageAmount -= this.defense;
            }
        }
        public override void OnLevelUp()
        {
            int level;
            level = GetLevel(owner);
            this.defense = this.effect0[level];
            SetBuffToolTipVar(1, this.defense);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
        }
    }
}