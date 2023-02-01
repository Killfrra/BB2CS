#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SummonerTestForceSpell : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 3000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                level = GetSlotSpellLevel((ObjAIBase)unit, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 0)
                {
                    IncSpellLevel((ObjAIBase)unit, 0, SpellSlotType.SpellSlots);
                }
                SpellCast((ObjAIBase)unit, owner, default, default, 0, SpellSlotType.SpellSlots, 0, true, true, false, false, false, false);
            }
        }
    }
}