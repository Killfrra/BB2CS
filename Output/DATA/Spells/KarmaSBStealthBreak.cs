#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KarmaSBStealthBreak : BBBuffScript
    {
        float cooldownToRestore;
        public KarmaSBStealthBreak(float cooldownToRestore = default)
        {
            this.cooldownToRestore = cooldownToRestore;
        }
        public override void OnActivate()
        {
            //RequireVar(this.cooldownToRestore);
            SetSlotSpellCooldownTimeVer2(this.cooldownToRestore, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}