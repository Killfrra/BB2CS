#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaSongofDiscordAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaSongofDiscordAura",
            BuffTextureName = "Sona_SongofDiscordGold.dds",
        };
        Particle songAura;
        float lastTimeExecuted;
        int[] effect0 = {8, 11, 14, 17, 20};
        public override void OnActivate()
        {
            SpellEffectCreate(out this.songAura, out _, "SonaSongofDiscord_aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.songAura);
        }
        public override void OnUpdateActions()
        {
            int level;
            float nextBuffVars_MSBoost;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MSBoost = this.effect0[level];
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.SonaSongofDiscordAuraB(nextBuffVars_MSBoost), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
        }
    }
}