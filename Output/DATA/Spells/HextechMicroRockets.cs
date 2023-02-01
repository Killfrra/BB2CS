#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HextechMicroRockets : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        Region bubbleID; // UNUSED
        public override bool CanCast()
        {
            bool returnValue = true;
            bool temp;
            bool result;
            temp = false;
            foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 3, default, true))
            {
                result = CanSeeTarget(owner, unit);
                if(result)
                {
                    temp = true;
                }
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
            TeamId casterID;
            bool result;
            casterID = GetTeamID(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UpgradeBuff)) > 0)
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 5, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if(result)
                    {
                        this.bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            else
            {
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 3, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if(result)
                    {
                        this.bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
        }
    }
}