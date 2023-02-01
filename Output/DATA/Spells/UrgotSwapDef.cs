#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrgotSwapDef : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "UrgotSwapDef",
            BuffTextureName = "UrgotPositionReverser.dds",
        };
        float defInc;
        Particle particle1;
        public UrgotSwapDef(float defInc = default)
        {
            this.defInc = defInc;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle1, out _, "UrgotSwapDef.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.defInc);
            IncFlatSpellBlockMod(owner, this.defInc);
        }
    }
}