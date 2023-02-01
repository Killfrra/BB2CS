#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RegenerationRuneAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "regen_rune_new_buf.troy", },
            BuffName = "RegenerationAura",
            BuffTextureName = "Regeneration_Sigil.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            float gameTime;
            teamID = GetTeamID(owner);
            gameTime = GetGameTime();
            if(gameTime >= 210)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.RegenerationRune(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            float gameTime;
            if(ExecutePeriodically(5, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RegenerationRune)) == 0)
                {
                    gameTime = GetGameTime();
                    if(gameTime >= 210)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.RegenerationRune(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnDeath()
        {
            TeamId teamID;
            Vector3 castPos;
            Minion other2;
            if(attacker is not BaseTurret)
            {
                if(attacker is ObjAIBase)
                {
                    teamID = GetTeamID(attacker);
                    castPos = GetUnitPosition(owner);
                    other2 = SpawnMinion("k", "TestCubeRender", "idle.lua", castPos, teamID, true, true, false, true, true, true, 0, false, true);
                    AddBuff(attacker, other2, new Buffs.ExpirationTimer(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SetSpell(other2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.MonsterRegenSpell));
                    SpellCast(other2, attacker, castPos, default, 0, SpellSlotType.SpellSlots, 1, false, true, false, false, false, true, castPos);
                }
            }
        }
    }
}