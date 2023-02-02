#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Malice_markertwo : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeath()
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.SilentKiller)) > 0)
                {
                    int level;
                    level = GetSlotSpellLevel((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level > 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    }
                }
            }
        }
    }
}