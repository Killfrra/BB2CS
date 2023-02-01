#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XenZhaoSweepArmor : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoSweepArmor",
            BuffTextureName = "XenZhao_CrescentSweepNew.dds",
        };
        float totalArmor;
        Particle hi;
        public XenZhaoSweepArmor(float totalArmor = default)
        {
            this.totalArmor = totalArmor;
        }
        public override void OnActivate()
        {
            int level; // UNUSED
            int xZSkinID;
            //RequireVar(this.totalArmor);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            xZSkinID = GetSkinID(owner);
            if(xZSkinID == 3)
            {
                SpellEffectCreate(out this.hi, out _, "xenZiou_SelfShield_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, default, default, false);
            }
            else
            {
                SpellEffectCreate(out this.hi, out _, "xenZiou_SelfShield_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CENTER_LOC", default, owner, default, default, false, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.hi);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.totalArmor);
        }
    }
}