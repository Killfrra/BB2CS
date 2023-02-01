#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GalioBulwarkHeal : BBBuffScript
    {
        float healAmount;
        public GalioBulwarkHeal(float healAmount = default)
        {
            this.healAmount = healAmount;
        }
        public override void OnActivate()
        {
            Particle healVFX; // UNUSED
            //RequireVar(this.healAmount);
            IncHealth(owner, this.healAmount, owner);
            SpellEffectCreate(out healVFX, out _, "galio_bulwark_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
    }
}