#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrBearActivation : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "BearStance.troy", },
            BuffName = "UdyrBearActivation",
            BuffTextureName = "Udyr_BearStance.dds",
        };
        float moveSpeedMod;
        Particle bearparticle;
        float[] effect0 = {0.15f, 0.18f, 0.21f, 0.24f, 0.27f};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.moveSpeedMod = this.effect0[level];
            SpellEffectCreate(out this.bearparticle, out _, "PrimalCharge.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.bearparticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}