#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyDITargetDmg : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyDiplomaticImmunityDmg",
            BuffTextureName = "Poppy_DiplomaticImmunity.dds",
        };
        float[] effect0 = {1.2f, 1.3f, 1.4f};
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.PoppyDITarget)) > 0)
            {
                int level;
                float levelMultiplier;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                levelMultiplier = this.effect0[level];
                damageAmount *= levelMultiplier;
            }
        }
    }
}