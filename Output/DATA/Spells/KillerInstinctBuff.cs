#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KillerInstinctBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KillerInstinct",
            BuffTextureName = "Katarina_KillerInstincts.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        float bonusDamage;
        int[] effect0 = {8, 12, 16, 20, 24};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.bonusDamage = this.effect0[level];
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            damageAmount += this.bonusDamage;
        }
    }
}