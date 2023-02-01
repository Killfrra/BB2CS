#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RelentlessAssaultMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Armsmaster_CoupDeGrace.dds",
        };
        bool isActive;
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, level, false);
            this.isActive = false;
        }
        public override void OnDeactivate(bool expired)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Empower)) > 0)
            {
                OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            else
            {
                RemoveOverrideAutoAttack(owner, false);
            }
        }
        public override void OnUpdateStats()
        {
            this.isActive = true;
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(this.isActive)
            {
                SpellBuffRemove(owner, nameof(Buffs.RelentlessAssaultMarker), (ObjAIBase)owner);
                SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.RelentlessAssaultDebuff), 0);
            }
        }
        public override void OnPreAttack()
        {
            if(target is not ObjAIBase)
            {
                SpellBuffRemoveCurrent(owner);
            }
            if(target is BaseTurret)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}