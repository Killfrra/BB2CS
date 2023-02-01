#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShrineTimeBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineTimeBuff",
            BuffTextureName = "Chronokeeper_Haste.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Particle buffParticle;
        public override void OnActivate()
        {
            float ultCD;
            float newUltCD;
            float sS0CD;
            float newSS0CD;
            float sS1CD;
            float newSS1CD;
            ultCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            newUltCD = ultCD / 2;
            SetSlotSpellCooldownTimeVer2(newUltCD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            sS0CD = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            newSS0CD = sS0CD / 2;
            SetSlotSpellCooldownTimeVer2(newSS0CD, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner);
            sS1CD = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            newSS1CD = sS1CD / 2;
            SetSlotSpellCooldownTimeVer2(newSS1CD, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner);
            SpellEffectCreate(out this.buffParticle, out _, "NeutralMonster_buf_blue_defense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
    }
}