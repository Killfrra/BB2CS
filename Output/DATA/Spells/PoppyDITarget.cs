#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyDITarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "DiplomaticImmunity_tar.troy", },
            BuffName = "PoppyDITarget",
            BuffTextureName = "Poppy_DiplomaticImmunity.dds",
            NonDispellable = true,
        };
        int[] effect0 = {6, 7, 8};
        public override void OnActivate()
        {
            ObjAIBase caster;
            int level;
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel(caster, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            AddBuff((ObjAIBase)owner, caster, new Buffs.PoppyDiplomaticImmunity(), 1, 1, this.effect0[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true);
        }
        public override void OnDeath()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(GetBuffCountFromCaster(caster, owner, nameof(Buffs.PoppyDiplomaticImmunity)) > 0)
            {
                SpellBuffRemove(caster, nameof(Buffs.PoppyDiplomaticImmunity), (ObjAIBase)owner);
            }
        }
    }
}