#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Feast_internal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float[] effect0 = {0.07f, 0.11f, 0.15f};
        float[] effect1 = {0.07f, 0.11f, 0.15f};
        int[] effect2 = {90, 120, 150};
        public override void OnActivate()
        {
            int level;
            float sizeByLevel; // UNUSED
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            sizeByLevel = this.effect0[level];
            IncScaleSkinCoef(0.5f, owner);
        }
        public override void OnUpdateStats()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
            if(count == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                int level;
                float sizeByLevel;
                float bonus;
                float healthPerStack;
                float bonusHealth;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                sizeByLevel = this.effect1[level];
                bonus = count * sizeByLevel;
                IncScaleSkinCoef(bonus, owner);
                healthPerStack = this.effect2[level];
                bonusHealth = healthPerStack * count;
                IncFlatHPPoolMod(owner, bonusHealth);
            }
        }
    }
}