#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CounterStrikeCanCast : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Counter Strike Can Cast",
            BuffTextureName = "Armsmaster_Disarm.dds",
            NonDispellable = true,
        };
        Particle removeMe2;
        bool cooledDown;
        Particle removeMe;
        public override void OnActivate()
        {
            TeamId teamID;
            float cooldown;
            teamID = GetTeamID(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(cooldown <= 0)
            {
                SpellEffectCreate(out this.removeMe2, out _, "CounterStrike_ready.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                this.cooledDown = true;
            }
            else
            {
                SpellEffectCreate(out this.removeMe, out _, "CounterStrike_dodged.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                this.cooledDown = false;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            if(!this.cooledDown)
            {
                SpellEffectRemove(this.removeMe);
            }
            else
            {
                SpellEffectRemove(this.removeMe2);
            }
        }
        public override void OnUpdateStats()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            if(!this.cooledDown)
            {
                float cooldown;
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(cooldown <= 0)
                {
                    this.cooledDown = true;
                    SpellEffectRemove(this.removeMe);
                    SpellEffectCreate(out this.removeMe2, out _, "CounterStrike_ready.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                }
            }
        }
    }
}