#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxPrismaticPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float cooldownBonus;
        float[] effect0 = {-0.02f, -0.04f, -0.06f, -0.08f, -0.1f};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.cooldownBonus = this.effect0[level];
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, this.cooldownBonus);
        }
    }
}