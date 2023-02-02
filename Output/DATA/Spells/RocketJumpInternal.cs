#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RocketJumpInternal : BBBuffScript
    {
        public override void OnDeath()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(caster.IsDead)
            {
            }
            else
            {
                int level;
                level = GetSlotSpellLevel(caster, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level >= 1)
                {
                    float cooldown;
                    cooldown = GetSlotSpellCooldownTime(caster, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown > 0)
                    {
                        SetSlotSpellCooldownTime(caster, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    }
                }
            }
        }
    }
}