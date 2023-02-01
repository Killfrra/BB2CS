#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonRageReady : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "Renekton_Predator.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 1,
        };
        Particle rHand;
        Particle lHand;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.rHand, out _, "Renekton_Rage_State.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_Hand", default, target, default, default, false);
            SpellEffectCreate(out this.lHand, out _, "Renekton_Rage_State.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_Hand", default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.lHand);
            SpellEffectRemove(this.rHand);
        }
    }
}