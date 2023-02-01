#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AhriOrbMissile : BBSpellScript
    {
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            int nextBuffVars_OrbofDeceptionIsActive;
            missileEndPosition = ModifyPosition(0, -50, 0);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, missileEndPosition, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                nextBuffVars_OrbofDeceptionIsActive = charVars.OrbofDeceptionIsActive;
                AddBuff(attacker, unit, new Buffs.AhriOrbDamage(nextBuffVars_OrbofDeceptionIsActive), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, missileEndPosition, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                nextBuffVars_OrbofDeceptionIsActive = charVars.OrbofDeceptionIsActive;
                AddBuff(attacker, unit, new Buffs.AhriOrbDamageSilence(nextBuffVars_OrbofDeceptionIsActive), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SpellCast((ObjAIBase)owner, owner, default, default, 1, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, missileEndPosition);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_OrbofDeceptionIsActive;
            nextBuffVars_OrbofDeceptionIsActive = charVars.OrbofDeceptionIsActive;
            AddBuff((ObjAIBase)owner, target, new Buffs.AhriOrbDamage(nextBuffVars_OrbofDeceptionIsActive), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}