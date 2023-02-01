#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HateSpike : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool temp;
            temp = false;
            foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                temp = true;
            }
            if(temp)
            {
                returnValue = true;
            }
            else
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 355, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, 1, default, true))
            {
                SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
        }
    }
}