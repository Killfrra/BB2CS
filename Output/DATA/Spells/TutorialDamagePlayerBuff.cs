#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TutorialDamagePlayerBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "FallenOne_tar.troy", },
            BuffName = "FallenOne",
            BuffTextureName = "Lich_DeathRay.dds",
        };
        public override void OnDeactivate(bool expired)
        {
            Particle particle; // UNUSED
            SpellEffectCreate(out particle, out _, "FallenOne_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            ApplyDamage((ObjAIBase)owner, owner, 200, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1);
        }
    }
}