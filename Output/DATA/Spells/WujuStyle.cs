#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WujuStyle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weaponstreak", },
            AutoBuffActivateEffect = new[]{ "Wujustyle_buf.troy", },
            BuffName = "Wuju Style",
            BuffTextureName = "MasterYi_SunderingStrikes.dds",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.MasterYiWujuDeactivated), (ObjAIBase)owner);
        }
        public override void OnUpdateStats()
        {
            int level;
            float baseDamage;
            float levelDamage;
            float totalDamage;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = 10;
            levelDamage = 5 * level;
            totalDamage = levelDamage + baseDamage;
            IncFlatPhysicalDamageMod(owner, totalDamage);
        }
    }
}
namespace Spells
{
    public class WujuStyle : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.WujuStyle), (ObjAIBase)owner);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, owner, new Buffs.WujuStyleSuperCharged(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}