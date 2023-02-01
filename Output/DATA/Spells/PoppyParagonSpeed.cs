#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyParagonSpeed : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "PoppyParagonSpeed",
            BuffTextureName = "Poppy_MightOfDemacia.dds",
        };
        Particle speedParticle;
        float moveSpeedVar;
        float[] effect0 = {0.17f, 0.19f, 0.21f, 0.23f, 0.25f};
        public override void OnActivate()
        {
            int level;
            SpellEffectCreate(out this.speedParticle, out _, "Global_Haste.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.moveSpeedVar = this.effect0[level];
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.speedParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedVar);
        }
    }
}