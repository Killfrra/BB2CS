#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyMightOfDemacia : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyMightOfDemacia",
            BuffTextureName = "Poppy_MightOfDemacia.dds",
        };
        float damageCount;
        float increasedDamage;
        float[] effect0 = {1, 1.5f, 2, 2.5f, 3};
        public PoppyMightOfDemacia(float damageCount = default)
        {
            this.damageCount = damageCount;
        }
        public override void OnActivate()
        {
            int level;
            float dmgPerHit;
            //RequireVar(this.damageCount);
            if(this.damageCount == 20)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyMightParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            dmgPerHit = this.effect0[level];
            this.increasedDamage = this.damageCount * dmgPerHit;
            SetBuffToolTipVar(1, this.damageCount);
            SetBuffToolTipVar(2, this.increasedDamage);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.increasedDamage);
        }
    }
}