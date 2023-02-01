#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShatterSelfBonus : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShatterSelfBonus",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
        Particle taric;
        float armorBonus;
        int[] effect0 = {10, 15, 20, 25, 30};
        public override void OnActivate()
        {
            SpellEffectCreate(out this.taric, out _, "ShatterReady_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.taric);
        }
        public override void OnUpdateStats()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.armorBonus = this.effect0[level];
            IncFlatArmorMod(owner, this.armorBonus);
        }
    }
}