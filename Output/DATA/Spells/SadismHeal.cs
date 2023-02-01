#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SadismHeal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        float lastTimeExecuted;
        float[] effect0 = {0.01667f, 0.02292f, 0.02917f};
        public override void OnUpdateStats()
        {
            int level;
            float maxHealth;
            float factor;
            float heal;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            factor = this.effect0[level];
            heal = factor * maxHealth;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                IncHealth(owner, heal, owner);
            }
        }
    }
}