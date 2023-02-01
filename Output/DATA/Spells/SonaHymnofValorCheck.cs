#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaHymnofValorCheck : BBBuffScript
    {
        Particle particleID;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.particleID, out _, "SonaPowerChordReady_blue.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            SpellBuffRemove(owner, nameof(Buffs.SonaAriaofPerseveranceCheck), (ObjAIBase)owner);
            SpellBuffRemove(owner, nameof(Buffs.SonaSongofDiscordCheck), (ObjAIBase)owner);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.SonaHymnofValorAttackUpgrade));
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleID);
        }
    }
}