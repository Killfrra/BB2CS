#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WujuStyleSuperCharged : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weaponstreak", },
            AutoBuffActivateEffect = new[]{ "WujustyleSC_buf.troy", },
            BuffName = "Wuju Style",
            BuffTextureName = "MasterYi_SunderingStrikes.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MasterYiWujuDeactivated(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
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