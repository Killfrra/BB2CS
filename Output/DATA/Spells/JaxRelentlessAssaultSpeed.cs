#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JaxRelentlessAssaultSpeed : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", },
            AutoBuffActivateEffect = new[]{ "JaxRelentlessAssault_buf.troy", "", },
            BuffName = "RelentlessBarrier",
            BuffTextureName = "Armsmaster_CoupDeGrace.dds",
        };
        float bonusAP;
        float bonusAD;
        int[] effect0 = {25, 45, 65};
        float[] effect1 = {0.2f, 0.2f, 0.2f};
        public override void OnActivate()
        {
            int level;
            float bonusADAP;
            float totalAD;
            float baseAD;
            float bonusAD;
            float bonusAP;
            float multiplier;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusADAP = this.effect0[level];
            totalAD = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            bonusAD = totalAD - baseAD;
            bonusAP = GetFlatMagicDamageMod(owner);
            multiplier = this.effect1[level];
            bonusAD *= multiplier;
            bonusAP *= multiplier;
            this.bonusAP = bonusAP + bonusADAP;
            this.bonusAD = bonusAD + bonusADAP;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, this.bonusAP);
            IncFlatPhysicalDamageMod(owner, this.bonusAD);
        }
    }
}