#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PropelTimer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            foreach(AttackableUnit unit in GetRandomUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                Vector3 propelPos;
                TeamId teamID; // UNUSED
                Minion other1;
                Particle effectToRemove; // UNUSED
                propelPos = GetRandomPointInAreaUnit(unit, 100, 25);
                teamID = GetTeamID(owner);
                other1 = SpawnMinion("DontSeeThisPlease", "SpellBook1", "idle.lua", propelPos, TeamId.TEAM_NEUTRAL, false, true, false, true, false, false, 0, default, true);
                AddBuff((ObjAIBase)owner, other1, new Buffs.PropelSpellCaster(), 1, 1, 2.1f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                SpellEffectCreate(out effectToRemove, out _, "PropelBubbles.troy", default, TeamId.TEAM_NEUTRAL, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, default, propelPos, target, default, default, true);
            }
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                FaceDirection(owner, unit.Position);
                SpellCast((ObjAIBase)owner, unit, owner.Position, owner.Position, 3, SpellSlotType.SpellSlots, 1, false, false, false, false, default, false);
            }
        }
    }
}