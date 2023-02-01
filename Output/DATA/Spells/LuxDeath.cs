#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxDeath : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnDeath()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.LuxDeathParticleTimer(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}