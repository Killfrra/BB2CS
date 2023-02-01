#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorPowerTransferBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "spine", },
            AutoBuffActivateEffect = new[]{ "HexMageTEMPQDEBUFF.troy", },
            BuffName = "Haste",
            BuffTextureName = "Averdrian_ConsumeSpirit.dds",
        };
        float[] effect0 = {-0.03f, -0.06f, -0.09f, -0.12f, -0.15f};
        public override void OnUpdateStats()
        {
            ObjAIBase caster;
            int level;
            float percMaxHealthMod;
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel(caster, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            percMaxHealthMod = this.effect0[level];
            IncPercentHPPoolMod(owner, percMaxHealthMod);
        }
        public override void OnDeath()
        {
            ObjAIBase other3;
            Vector3 targetPos; // UNUSED
            Vector3 ownerPos;
            other3 = SetBuffCasterUnit();
            targetPos = GetUnitPosition(other3);
            ownerPos = GetUnitPosition(owner);
            SpellCast(other3, other3, default, default, 2, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, ownerPos);
        }
    }
}