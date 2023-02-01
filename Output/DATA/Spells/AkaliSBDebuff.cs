#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliSBDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Slow.troy", },
            BuffName = "AkaliTwilightShroudDebuff",
            BuffTextureName = "AkaliTwilightShroud.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float movementSpeed;
        float attackSpeed;
        float[] effect0 = {-0.14f, -0.18f, -0.22f, -0.26f, -0.3f};
        int[] effect1 = {0, 0, 0, 0, 0};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.movementSpeed = this.effect0[level];
            this.attackSpeed = this.effect1[level];
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeed);
            IncPercentMultiplicativeAttackSpeedMod(owner, this.attackSpeed);
        }
    }
}