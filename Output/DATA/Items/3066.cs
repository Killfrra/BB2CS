#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3066 : BBItemScript
    {
        public override void UpdateAura()
        {
            DefUpdateAura(owner.Position, 200, UNITSCAN_Friends, nameof(Buffs.Fervor));
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "Fervor", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
    }
}
namespace Buffs
{
    public class _3066 : BBBuffScript
    {
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "Fervor", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
    }
}