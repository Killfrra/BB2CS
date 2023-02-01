#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneShroudofDarknessBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_hand", "L_hand", "", },
            AutoBuffActivateEffect = new[]{ "nocturne_shroud_AttackSpeed_buff.troy", "nocturne_shroud_AttackSpeed_buff.troy", "", },
            BuffName = "NocturneShroudofDarkness",
            BuffTextureName = "Nocturne_ShroudofDarkness.dds",
        };
        float attackSpeedBoost;
        float[] effect0 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.attackSpeedBoost = this.effect0[level];
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedBoost);
        }
    }
}