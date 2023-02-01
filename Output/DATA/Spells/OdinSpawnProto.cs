#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinSpawnProto : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinSpawnProto",
            BuffTextureName = "Summoner_spawn.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeath()
        {
            string name1;
            string name2;
            name1 = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(name1 == nameof(Spells.SummonerSpawn))
            {
                SetSlotSpellCooldownTimeVer2(24, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner);
            }
            if(name2 == nameof(Spells.SummonerSpawn))
            {
                SetSlotSpellCooldownTimeVer2(24, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_SUMMONER, (ObjAIBase)owner);
            }
        }
    }
}