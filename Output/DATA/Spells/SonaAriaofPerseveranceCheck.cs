#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaAriaofPerseveranceCheck : BBBuffScript
    {
        Particle particleID;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.particleID, out _, "SonaPowerChordReady_green.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellBuffRemove(owner, nameof(Buffs.SonaHymnofValorCheck), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.SonaSongofDiscordCheck), (ObjAIBase)owner, 0);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.SonaAriaofPerseveranceUpgrade));
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleID);
        }
    }
}