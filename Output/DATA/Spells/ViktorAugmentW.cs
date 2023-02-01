#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorAugmentW : BBBuffScript
    {
        public override void OnActivate()
        {
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ViktorGravitonFieldAugment));
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.1f);
        }
    }
}