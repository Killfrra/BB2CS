#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KillerInstinct : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KillerInstinct",
            BuffTextureName = "Katarina_KillerInstincts.dds",
        };
        Particle kIRHand;
        Particle kILHand;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.kIRHand, out _, "katarina_killerInstinct_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.kILHand, out _, "katarina_killerInstinct_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.kILHand);
            SpellEffectRemove(this.kIRHand);
        }
    }
}
namespace Spells
{
    public class KillerInstinct : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.KillerInstinct(), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}