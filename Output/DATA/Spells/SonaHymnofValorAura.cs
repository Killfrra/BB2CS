#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaHymnofValorAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaHymnofValorAura",
            BuffTextureName = "Sona_HymnofValorGold.dds",
        };
        Particle hymnAura;
        float lastTimeExecuted;
        int[] effect0 = {8, 11, 14, 17, 20};
        public override void OnActivate()
        {
            SpellEffectCreate(out this.hymnAura, out _, "SonaHymnofValor_aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.hymnAura);
        }
        public override void OnUpdateActions()
        {
            int level;
            float nextBuffVars_APADBoost;
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_APADBoost = this.effect0[level];
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.SonaHymnofValorAuraB(nextBuffVars_APADBoost), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
    }
}