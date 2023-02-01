#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AlphaStrikeTeleport : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 pos;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, nameof(Buffs.AlphaStrikeMarker), true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.AlphaStrikeMarker)) > 0)
                {
                    pos = GetPointByUnitFacingOffset(unit, 75, 0);
                    TeleportToPosition(owner, pos);
                    if(unit is Champion)
                    {
                        IssueOrder(owner, OrderType.AttackTo, default, unit);
                    }
                    SpellBuffRemove(unit, nameof(Buffs.AlphaStrikeMarker), (ObjAIBase)owner, 0);
                    SpellBuffRemove(owner, nameof(Buffs.AlphaStrikeMarker), (ObjAIBase)owner, 0);
                }
            }
        }
    }
}