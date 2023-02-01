#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XenZhaoBattleCryActivated : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", },
            AutoBuffActivateEffect = new[]{ "WujustyleSC_buf.troy", },
            BuffName = "Wuju Style",
            BuffTextureName = "XenZhao_BattleCry.dds",
        };
        public override void OnUpdateStats()
        {
            int level;
            float baseDamage;
            float levelDamage;
            float totalDamage;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = 20;
            levelDamage = 10 * level;
            totalDamage = levelDamage + baseDamage;
            IncFlatPhysicalDamageMod(owner, totalDamage);
        }
    }
}