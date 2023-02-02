#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KennenDoubleStrikeLive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "KennenDoubleStrikeLive",
            BuffTextureName = "Kennen_ElectricalSurge.dds",
        };
        Particle asdf1;
        public override void OnActivate()
        {
            TeamId teamID;
            int level;
            teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, level, true);
            SpellEffectCreate(out this.asdf1, out _, "kennen_ds_proc.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_hand", default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.asdf1);
        }
    }
}