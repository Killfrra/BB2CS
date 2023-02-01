#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XenZhaoBattleCryPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "XenZhaoBattleCryPassive",
            BuffTextureName = "XenZhao_BattleCry.dds",
        };
        Particle passivePart;
        float[] effect0 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        public override void OnActivate()
        {
            SpellEffectCreate(out this.passivePart, out _, "xen_ziou_battleCry_passive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.passivePart);
        }
        public override void OnUpdateStats()
        {
            int level;
            float aSAura;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            aSAura = this.effect0[level];
            IncPercentAttackSpeedMod(owner, aSAura);
        }
    }
}