#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XerathArcaneBarrageBarrage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XerathBarrage",
            BuffTextureName = "Xerath_ArcaneBarrage.dds",
        };
        int[] effect0 = {80, 70, 60, 40, 40};
        public override void OnDeactivate(bool expired)
        {
            int level;
            float spellCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.XerathArcaneBarrageBarrage)) > 0)
            {
            }
            else
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                spellCooldown = this.effect0[level];
                cooldownStat = GetPercentCooldownMod(owner);
                multiplier = 1 + cooldownStat;
                newCooldown = multiplier * spellCooldown;
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            }
        }
    }
}