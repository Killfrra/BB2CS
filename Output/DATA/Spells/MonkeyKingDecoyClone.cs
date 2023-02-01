#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingDecoyClone : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
        };
        Particle particle; // UNUSED
        public override void OnActivate()
        {
            TeamId teamID;
            SetNotTargetableToTeam(owner, true, false);
            ShowHealthBar(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            teamID = GetTeamID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "MonkeyKing_Copy.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_BLUE, default, default, true, owner, "root", default, target, "root", default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "MonkeyKing_Copy.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_PURPLE, default, default, true, owner, "root", default, target, "root", default, false, false, false, false, false);
            }
            IssueOrder(owner, OrderType.Hold, default, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SpellCast((ObjAIBase)owner, owner, default, default, 1, SpellSlotType.ExtraSlots, level, false, false, false, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MonkeyKingKillCloneW(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
        }
    }
}