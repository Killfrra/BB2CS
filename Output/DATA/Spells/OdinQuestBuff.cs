#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinQuestBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinQuestBuff",
            BuffTextureName = "Odin_MarkoftheConqueror.dds",
            NonDispellable = true,
        };
        Particle buffParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.buffParticle, out _, "odin_quest_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= 1.1f;
        }
    }
}